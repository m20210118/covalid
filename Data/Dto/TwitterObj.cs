using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covalid.Data.Dto
{
    public class TwitterDto
    {
        public List<Datum> data { get; set; }
        public List<ApiError> errors { get; set; }
    }

    public class Datum
    {
        public string id { get; set; }
        public string author_id { get; set; }
        public DateTime created_at { get; set; }
        public string text { get; set; }
        public bool possibly_sensitive { get; set; }
        public List<ReferencedTweets> referenced_tweets { get; set; }
        public Withheld withheld { get; set; }
        public PublicMetrics public_metrics { get; set; }
        public Entities entities { get; set; }
        public List<ContextAnnotation> context_annotations { get; set; }
    }

    public class ApiError
    {
        public string value { get; set; }
        public string detail { get; set; }
        public string title { get; set; }
        public string resource_type { get; set; }
        public string parameter { get; set; }
        public string resource_id { get; set; }
        public string type { get; set; }
        public string section { get; set; }

    }

    public class Domain
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }


    public class Entity
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class ContextAnnotation
    {
        public Domain domain { get; set; }
        public Entity entity { get; set; }
    }

    public class Mention
    {
        public string id { get; set; }
        public string username { get; set; }
        public Int32 start { get; set; }
        public Int32 end { get; set; }
    }

    public class Annotation
    {
        public string type { get; set; }
        public string normalized_text { get; set; }
        public Int32 start { get; set; }
        public Int32 end { get; set; }
        public Decimal probability { get; set; }
    }

    public class Entities
    {
        public List<Mention> mentions { get; set; }
        public List<Annotation> annotations { get; set; }
        public List<Hashtag> hashtags { get; set; }
    }

    public class Hashtag
    {
        public string tag { get; set; }
        public Int32 start { get; set; }
        public Int32 end { get; set; }
    }

    public class PublicMetrics
    {
        public Int32 retweet_count { get; set; }
        public Int32 reply_count { get; set; }
        public Int32 like_count { get; set; }
        public Int32 quote_count { get; set; }
    }

    public class Withheld
    {
        public bool copyright { get; set; }
        public string scope { get; set; }
    }

    public class ReferencedTweets
    {
        public string type { get; set; }
        public string id { get; set; }
    }
}