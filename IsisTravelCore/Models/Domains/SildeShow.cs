using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IsisTravelCore.Models.Domains
{
    public class SildeShow
    {
        public int Id { get; set; }
        [Display(Name = "Image", ResourceType = typeof(Resources.SildeShow))]
        public string Image { get; set; }
        [Column(TypeName = "text")]
        [Display(Name = "Description", ResourceType = typeof(Resources.Service))]
        public string Description { get; set; }
        [Display(Name = "Tour", ResourceType = typeof(Resources.SildeShow))]
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Resources.Activity))]
        public bool Active { get; set; }
        [Display(Name = "Creator", ResourceType = typeof(Resources.Activity))]
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "CreatedDate", ResourceType = typeof(Resources.Activity))]
        public DateTime CreatedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "LastModifiedDate", ResourceType = typeof(Resources.Activity))]
        public DateTime LastModifiedDate { get; set; }
    }
}
