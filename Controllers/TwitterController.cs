using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Covalid.Data;
using Covalid.Data.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Covalid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TwitterController : ControllerBase
    {
        private DataContext _context;

        public TwitterController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetMultipleTweets")]
        public async Task<IActionResult> GetMultipleTweets([FromQuery] string tweetIds)
        {
            string responseBody = null;

            if (tweetIds != null)
            {
                string tweetFields = "tweet.fields=author_id,context_annotations,created_at,entities,possibly_sensitive,public_metrics,withheld";
                var webEndpoint = WebRequest.Create("https://api.twitter.com/2/tweets?ids=" + tweetIds + "&" + tweetFields) as HttpWebRequest;

                webEndpoint.Method = "GET";
                webEndpoint.Headers[HttpRequestHeader.Authorization] = "Bearer ";   //Add bearer token here

                using (var response = webEndpoint.GetResponse().GetResponseStream())
                {
                    var respR = new StreamReader(response);
                    responseBody = respR.ReadToEnd();
                }
            }
            var datum = JsonConvert.DeserializeObject<TwitterDto>(responseBody);
            return Ok(datum);
        }

        [HttpGet("DownloadTweets")]
        public async Task<IActionResult> DownloadTweets([FromQuery] int noOfIterations)
        {
            int successCount = 0;
            int errorCount = 0;

            for (var i = 0; i < noOfIterations; i++)
            {
                string responseBody = null;
                var tweetIdList = await (from o in _context.TwtOriginal select o.id).Take(100).ToListAsync();
                string tweetIds = null;

                foreach (var id in tweetIdList)
                {
                    if (id != tweetIdList.Last())
                        tweetIds += id + ",";
                    else
                        tweetIds += id;
                }

                if (tweetIds != null)
                {
                    string tweetFields = "tweet.fields=author_id,context_annotations,created_at,entities,referenced_tweets,possibly_sensitive,public_metrics,withheld";
                    var webEndpoint = WebRequest.Create("https://api.twitter.com/2/tweets?ids=" + tweetIds + "&" + tweetFields) as HttpWebRequest;

                    webEndpoint.Method = "GET";
                    webEndpoint.Headers[HttpRequestHeader.Authorization] = "Bearer "; //Add bearer token here

                    using (var response = webEndpoint.GetResponse().GetResponseStream())
                    {
                        var respR = new StreamReader(response);
                        responseBody = respR.ReadToEnd();
                    }
                }

                var datum = JsonConvert.DeserializeObject<TwitterDto>(responseBody);

                if (datum.data != null)
                {
                    foreach (var tweet in datum.data)
                    {
                        if (tweet.referenced_tweets == null || tweet.referenced_tweets.Count == 0)
                        {
                            successCount += 1;

                            TwtData data = new TwtData
                            {
                                id = tweet.id,
                                text = tweet.text ?? null,
                                author_id = tweet.author_id ?? null,
                                created_at = tweet.created_at,
                                possibly_sensitive = tweet.possibly_sensitive,
                                withheld = tweet.withheld != null,
                                retweet_count = tweet.public_metrics.retweet_count,
                                reply_count = tweet.public_metrics.reply_count,
                                like_count = tweet.public_metrics.like_count,
                                quote_count = tweet.public_metrics.quote_count
                            };
                            _context.TwtData.Add(data);

                            if (tweet.context_annotations != null)
                            {
                                foreach (var context in tweet.context_annotations)
                                {
                                    if (context.domain.name != null)
                                    {
                                        TwtContext newContextDomain = new TwtContext
                                        {
                                            id = _context.GetSeq("TwtContextId"),
                                            tweetId = tweet.id,
                                            annotation_type = "domain",
                                            name = context.domain.name,
                                            description = context.domain.description ?? null

                                        };
                                        _context.TwtContext.Add(newContextDomain);
                                    }

                                    if (context.entity.name != null)
                                    {
                                        TwtContext newContextEntity = new TwtContext
                                        {
                                            id = _context.GetSeq("TwtContextId"),
                                            tweetId = tweet.id,
                                            annotation_type = "entity",
                                            name = context.entity.name,
                                            description = context.entity.description ?? null
                                        };
                                        _context.TwtContext.Add(newContextEntity);
                                    }
                                }
                            }

                            // if (tweet.withheld != null)
                            // {
                            //     TwtWithheld newWithheldData = new TwtWithheld
                            //     {
                            //         id = _context.GetSeq("TwtWithheldId"),
                            //         tweetId = tweet.id,
                            //         copyright = tweet.withheld.copyright,
                            //         scope = tweet.withheld.scope
                            //     };
                            //     _context.TwtWithheld.Add(newWithheldData);
                            // }

                            var existingTweet = await (from o in _context.TwtOriginal where o.id == tweet.id select o).FirstOrDefaultAsync();
                            _context.TwtOriginal.Remove(existingTweet);
                        }
                        else
                        {
                            var existingTweet = await (from o in _context.TwtOriginal where o.id == tweet.id select o).FirstOrDefaultAsync();
                            _context.TwtOriginal.Remove(existingTweet);
                        }
                    }
                    await _context.SaveChangesAsync();
                }

                if (datum.errors != null)
                {
                    errorCount += datum.errors.Count;

                    foreach (var error in datum.errors)
                    {
                        var existingTweet = await (from o in _context.TwtOriginal where o.id == error.value select o).FirstOrDefaultAsync();
                        _context.TwtOriginal.Remove(existingTweet);
                    }
                    await _context.SaveChangesAsync();
                }
            }

            var result = new { successfulTweet = successCount, errorTweets = errorCount };
            return Ok(result);
        }


        [HttpPost("InsertLog")]
        public async Task<IActionResult> InsertLog([FromBody] ValidateDto userText)
        {
            UserLog userLog = new UserLog()
            {
                Text = userText.user_text,
                Real = userText.real,
                Fake = userText.fake,
                LogDate = DateTime.Now
            };
            _context.UserLog.Add(userLog);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}