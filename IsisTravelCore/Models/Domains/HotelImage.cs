using System;

namespace IsisTravelCore.Models.Domains
{
    public class HotelImage
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public string Image { get; set; }
        public bool IsHome { get; set; }
        public bool Active { get; set; }
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}