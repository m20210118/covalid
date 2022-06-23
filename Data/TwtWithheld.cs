using System;

namespace Covalid.Data
{
    public class TwtWithheld
    {
        public Int64 id { get; set; }
        public string tweetId { get; set; }
        public bool copyright { get; set; }
        public string scope { get; set; }
    }
}