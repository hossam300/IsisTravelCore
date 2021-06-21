using System;
using System.ComponentModel.DataAnnotations;

namespace IsisTravelCore.Models.Domains
{
    public class CatrgoryDayPrice
    {
        public int Id { get; set; }
        [Display(Name = "Date", ResourceType = typeof(Resources.Category))]
        [Required(ErrorMessage = "Fecha requerido")]
        public DateTime Date { get; set; }
        [Display(Name = "AdultPrice", ResourceType = typeof(Resources.Category))]
        [Required(ErrorMessage = "Precio adulto requerido")]
        public decimal AdultPrice { get; set; }
        [Required(ErrorMessage = "Precio del niño requerido")]
        [Display(Name = "ChildPrice", ResourceType = typeof(Resources.Category))]
        public decimal ChildPrice { get; set; }
        [Required(ErrorMessage = "Precio de bebe requerido")]
        [Display(Name = "BabyPrice", ResourceType = typeof(Resources.Category))]
        public decimal BabyPrice { get; set; }
        public decimal SingleRoomExtrafees { get; set; }
        public int TourCategoryId { get; set; }
        public TourCategory TourCategory { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Resources.Category))]

        public bool Active { get; set; }

        [Display(Name = "Creator", ResourceType = typeof(Resources.Category))]
        public string CreatorId { get; set; }
        [Display(Name = "Creator", ResourceType = typeof(Resources.Category))]
        public virtual ApplicationUser Creator { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "CreatedDate", ResourceType = typeof(Resources.Category))]
        public DateTime CreatedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "LastModifiedDate", ResourceType = typeof(Resources.Category))]
        public DateTime LastModifiedDate { get; set; }
    }
}