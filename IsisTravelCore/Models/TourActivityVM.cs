using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsisTravelCore.Models
{
    public class TourActivityVM
    {
        public int TourActivityId { get; set; }
        public int TourId { get; set; }
        public string ActivityId { get; set; }
        public string Description { get; set; }
        public int Activity { get;  set; }
    }
}
