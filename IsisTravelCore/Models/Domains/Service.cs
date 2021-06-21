using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IsisTravelCore.Models.Domains
{
    public class Service
    {
        public int Id { get; set; }
        
        [Display(Name = "Name", ResourceType = typeof(Resources.Service))]
        [Required(ErrorMessage = "nombre requerido")]
        public string Name { get; set; }
        [Column(TypeName = "text")]
        [Display(Name = "Description", ResourceType = typeof(Resources.Service))]
        public string Description { get; set; }
        [Display(Name = "Icon", ResourceType = typeof(Resources.Service))]
        public string Icon { get; set; }
        [Display(Name = "Price", ResourceType = typeof(Resources.Service))]
        public decimal? Price { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Resources.Service))]
        public bool Active { get; set; }
        [Display(Name = "Creator", ResourceType = typeof(Resources.Service))]
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "CreatedDate", ResourceType = typeof(Resources.Service))]
        public DateTime CreatedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "LastModifiedDate", ResourceType = typeof(Resources.Service))]
        public DateTime LastModifiedDate { get; set; }
    }
}