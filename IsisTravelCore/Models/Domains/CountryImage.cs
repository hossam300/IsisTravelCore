using System;

namespace IsisTravelCore.Models.Domains
{
    public class CountryImage
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public bool SlideShow { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public bool Active { get; set; }
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}