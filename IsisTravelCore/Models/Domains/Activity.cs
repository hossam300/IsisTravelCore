using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IsisTravelCore.Models.Domains
{
    public class Activity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "nombre requerido")]
        [Display(Name = "Name", ResourceType = typeof(Resources.Activity))]
        public string Name { get; set; }
        [Column(TypeName = "text")]
        [Display(Name = "Description", ResourceType = typeof(Resources.Activity))]
        public string Description { get; set; }
        [Display(Name = "MainImage", ResourceType = typeof(Resources.Activity))]
        public string Image1 { get; set; }
        [Display(Name = "SecondImage", ResourceType = typeof(Resources.Activity))]
        public string Image2 { get; set; }
        [Display(Name = "Day", ResourceType = typeof(Resources.Activity))]
        [Required(ErrorMessage = "Día requerido")]
        public Day Day { get; set; }
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