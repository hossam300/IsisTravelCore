using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IsisTravelCore.Models.Domains
{
    public class Country
    {
        public Country()
        {
            Tours = new List<Tour>();
            CountryImages = new List<CountryImage>();
            Hotels = new List<Hotel>();
        }
        public int Id { get; set; }
        [Display(Name = "Name", ResourceType = typeof(Resources.Country))]
        [Required(ErrorMessage = "País requerido")]
        public string Name { get; set; }
        [Display(Name = "CountryMainImage", ResourceType = typeof(Resources.Country))]
        public string CountryMainImage { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Resources.Country))]
        public string Description { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Resources.Country))]
        public bool Active { get; set; }
        [Display(Name = "Creator", ResourceType = typeof(Resources.Country))]
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        [Display(Name = "CreatedDate", ResourceType = typeof(Resources.Country))]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "LastModifiedDate", ResourceType = typeof(Resources.Country))]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastModifiedDate { get; set; }
        public virtual List<CountryImage> CountryImages { get; set; }
        public virtual List<Tour> Tours { get; set; }
        public virtual List<Hotel> Hotels { get; set; }
    }
}