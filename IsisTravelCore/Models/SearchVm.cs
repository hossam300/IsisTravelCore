using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsisTravelCore.Models
{
    public class SearchVm
    {
        public string Keywords { get; set; }
        public int? Country { get; set; }
        public int? Activity { get; set; }
        public int? Category { get; set; }
    }
}
