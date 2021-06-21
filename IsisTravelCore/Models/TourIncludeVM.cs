using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsisTravelCore.Models
{
    public class TourIncludeVM
    {
        public int TourIncludeId { get; set; }
        public int TourId { get; set; }
        public string IncludeId { get; set; }
        public string IncludeDescription { get; set; }
        public int Include { get; set; }
    }
}
