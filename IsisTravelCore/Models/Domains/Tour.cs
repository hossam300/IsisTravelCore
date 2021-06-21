
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IsisTravelCore.Models.Domains
{
    public class Tour
    {
        public Tour()
        {
            TourCategories = new List<TourCategory>();
            Activities = new List<TourActivity>();
            TourInclude = new List<TourInclude>();
            AdditionalServices = new List<TourService>();
            TourImages = new List<TourImage>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "nombre requerido")]
        [Display(Name = "TourName", ResourceType = typeof(Resources.Tour))]
        public string TourName { get; set; }
        [Display(Name = "TourMainImage", ResourceType = typeof(Resources.Tour))]
        public string TourMainImage { get; set; }
        [Column(TypeName = "text")]
        [Display(Name = "Intro", ResourceType = typeof(Resources.Tour))]
        public string Intro { get; set; }
        [Column(TypeName = "text")]
        [Display(Name = "Description", ResourceType = typeof(Resources.Tour))]
        public string Description { get; set; }
        [Required(ErrorMessage = "Precio anterior requerida")]
        [Display(Name = "OldPrice", ResourceType = typeof(Resources.Tour))]
        public decimal OldPrice { get; set; }
        [Required(ErrorMessage = "Precio requerida")]
        [Display(Name = "Price", ResourceType = typeof(Resources.Tour))]
        public decimal Price { get; set; }
        [Display(Name = "HomePage", ResourceType = typeof(Resources.Tour))]
        public bool HomePage { get; set; }
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString = "{0:dd-MMM-yyyy}")]
        [Display(Name = "StartDate", ResourceType = typeof(Resources.Tour))]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        [Required(ErrorMessage = "Fecha final requerida")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Duration", ResourceType = typeof(Resources.Tour))]
        public int? Duration { get; set; }
        [Display(Name = "Country", ResourceType = typeof(Resources.Tour))]
        [Required(ErrorMessage = "País requerido")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual List<TourCategory> TourCategories { get; set; }
        public virtual List<TourActivity> Activities { get; set; }
        public virtual List<TourInclude> TourInclude { get; set; }
        public virtual List<TourService> AdditionalServices { get; set; }
        public virtual List<TourQuote> TourQuotes { get; set; }
        public virtual List<TourImage> TourImages { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Resources.Tour))]
        public bool Active { get; set; }
        [Display(Name = "Creator", ResourceType = typeof(Resources.Tour))]
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "CreatedDate", ResourceType = typeof(Resources.Tour))]
        public DateTime CreatedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "LastModifiedDate", ResourceType = typeof(Resources.Tour))]
        public DateTime LastModifiedDate { get; set; }
    }
}