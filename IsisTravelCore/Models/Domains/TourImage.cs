using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsisTravelCore.Models.Domains
{
    public class TourImage
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        public string Image { get; set; }
        public bool IsHome { get; set; }
        public bool Active { get; set; }
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
