using IsisTravelCore.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsisTravelCore.Models
{
    public class CategoryDayPriceVM
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string className { get; set; }
        public string description { get; set; }
    }
    public class CategoryDayPricesVM
    {
        public List<CategoryDayPriceVM> CategoryDayPrices { get; set; }
        public CatrgoryDayPrice DayPrice { get; set; }
    }
}