using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IsisTravelCore.Models.Domains
{
    public class Include
    {
        public int Id { get; set; }
        [Display(Name = "Name", ResourceType = typeof(Resources.Include))]
        [Required(ErrorMessage = "nombre requerido")]
        public string Name { get; set; }
        [Column(TypeName = "text")]
        [Display(Name = "Description", ResourceType = typeof(Resources.Include))]
        public string Description { get; set; }
        [Display(Name = "Icon", ResourceType = typeof(Resources.Include))]
        public string Icon { get; set; }
        [Display(Name = "Price", ResourceType = typeof(Resources.Include))]
        public decimal? Price { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Resources.Include))]
        public bool Active { get; set; }
        [Display(Name = "Creator", ResourceType = typeof(Resources.Include))]
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "CreatedDate", ResourceType = typeof(Resources.Include))]
        public DateTime CreatedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "LastModifiedDate", ResourceType = typeof(Resources.Include))]
        public DateTime LastModifiedDate { get; set; }
    }
}