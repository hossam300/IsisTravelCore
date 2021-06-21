
using System;
using System.Collections.Generic;

namespace IsisTravelCore.Models.Domains
{
    public class TourCategory
    {
        public TourCategory()
        {
            CategoryDayPrices = new List<CatrgoryDayPrice>();
        }
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int TourId { get; set; }
        public virtual Tour Tour { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public virtual List<CatrgoryDayPrice> CategoryDayPrices { get; set; }
        public bool Active { get; set; }
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}