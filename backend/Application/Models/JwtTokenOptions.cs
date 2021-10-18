using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class JwtTokenOptions
    {
        public string Secret { get; set; }
        public int Minutes { get; set; }
        public int DaysRemembered { get; set; }
    }
}
