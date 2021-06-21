using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsisTravelCore.Models
{
    public class CategoryTourVM
    {
        public string CategoryName { get;  set; }
        public string CityName { get;  set; }
        public decimal AdultPrice { get;  set; }
        public decimal ChildPrice { get;  set; }
        public decimal BabyPrice { get;  set; }
        public decimal SingleRoomExtrafees { get;  set; }
        public bool Active { get;  set; }
        public int Id { get; internal set; }
    }
}
