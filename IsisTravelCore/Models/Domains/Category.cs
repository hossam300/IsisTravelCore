using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IsisTravelCore.Models.Domains
{
    public class Category
    {
        public Category()
        {
            Hotels = new List<Hotel>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "nombre requerido")]
        [Display(Name = "Name", ResourceType = typeof(Resources.Category))]
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "AdultPrice", ResourceType = typeof(Resources.Category))]
        [DisplayFormat(DataFormatString = "{0:0.0000}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Precio adulto requerido")]
        public decimal AdultPrice { get; set; }
        [Required(ErrorMessage = "Precio del niño requerido")]
        [DisplayFormat(DataFormatString = "{0:0.0000}", ApplyFormatInEditMode = true)]
        [Display(Name = "ChildPrice", ResourceType = typeof(Resources.Category))]
        public decimal ChildPrice { get; set; }
        [Required(ErrorMessage = "Precio de bebe requerido")]
        [DisplayFormat(DataFormatString = "{0:0.0000}", ApplyFormatInEditMode = true)]
        [Display(Name = "BabyPrice", ResourceType = typeof(Resources.Category))]
        public decimal BabyPrice { get; set; }
        public virtual List<Hotel> Hotels { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Resources.Category))]
        public bool Active { get; set; }
        [Display(Name = "Creator", ResourceType = typeof(Resources.Category))]
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "CreatedDate", ResourceType = typeof(Resources.Category))]
        public DateTime CreatedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "LastModifiedDate", ResourceType = typeof(Resources.Category))]
        public DateTime LastModifiedDate { get; set; }
    }
}