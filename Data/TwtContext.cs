using System;

namespace Covalid.Data
{
    public class TwtContext
    {
        public Int64 id { get; set; }
        public string tweetId { get; set; }
        public string annotation_type { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}