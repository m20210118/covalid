using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covalid.Data.Dto
{
    public class ValidateDto
    {
        public string user_text { get; set; }
        public decimal? real { get; set; }
        public decimal? fake { get; set; }
    }
}