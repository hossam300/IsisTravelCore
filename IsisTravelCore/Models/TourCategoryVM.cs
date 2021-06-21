using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsisTravelCore.Models
{
    public class TourCategoryVM
    {
        public int Id { get; set; }
        public string CategoryId { get; set; }
        public int Category { get; set; }
        public int TourId { get; set; }
        public string Days { get; set; }
        public int CityId { get; set; }
        public decimal? AdultPrice { get; set; }
        public decimal? BabyPrice { get; set; }
        public decimal? ChildPrice { get; set; }
        public decimal? SingleRoomExtrafees { get; set; }
    }
}
