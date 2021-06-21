using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsisTravelCore.Models
{
    public class SildeShowDTO
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string TourName { get; set; }
        public decimal TourPrice { get; set; }
        public int TourId { get; set; }
        public string Description { get; set; }
    }
}
