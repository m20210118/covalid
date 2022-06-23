using System;

namespace Covalid.Data
{
    public class UserLog
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public string Text { get; set; }
        public decimal? Real { get; set; }
        public decimal? Fake { get; set; }
        public DateTime? LogDate { get; set; }
    }
}