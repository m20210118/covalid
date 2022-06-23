using System;

namespace Covalid.Data
{
    public class TwtData
    {
        public string id { get; set; }
        public string text { get; set; }
        public string vector_text { get; set; }
        public string author_id { get; set; }
        public DateTime created_at { get; set; }
        public string reference_type { get; set; }
        public string reference_id { get; set; }
        public bool possibly_sensitive { get; set; }
        public bool withheld { get; set; }
        public int view_count { get; set; }
        public int retweet_count { get; set; }
        public int reply_count { get; set; }
        public int like_count { get; set; }
        public int quote_count { get; set; }
        public bool legit { get; set; }
        public decimal probability { get; set; }
    }
}