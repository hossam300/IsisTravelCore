using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IsisTravelCore.Models.Domains
{
    public class Hotel
    {
        public Hotel()
        {
            HotelImages = new List<HotelImage>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "nombre requerido")]
        [Display(Name = "Name", ResourceType = typeof(Resources.Hotel))]
        public string Name { get; set; }
        [Display(Name = "HotelMainImage", ResourceType = typeof(Resources.Hotel))]
        public string HotelMainImage { get; set; }
        [Column(TypeName = "text")]
        [Display(Name = "Address", ResourceType = typeof(Resources.Hotel))]
        public string Address { get; set; }
        [Display(Name = "Phone", ResourceType = typeof(Resources.Hotel))]
        public string Phone { get; set; }
        [Column(TypeName = "text")]
        [Display(Name = "Description", ResourceType = typeof(Resources.Hotel))]
        public string Description { get; set; }
        [Display(Name = "ExtelnalLink", ResourceType = typeof(Resources.Hotel))]
        public string ExtelnalLink { get; set; }
        [Display(Name = "FacebookLink", ResourceType = typeof(Resources.Hotel))]
        public string FacebookLink { get; set; }
        [Display(Name = "TwitterLink", ResourceType = typeof(Resources.Hotel))]
        public string TwitterLink { get; set; }
        [Display(Name = "Lat", ResourceType = typeof(Resources.Hotel))]
        public decimal? Lat { get; set; }
        [Display(Name = "Lang", ResourceType = typeof(Resources.Hotel))]
        public decimal? Lang { get; set; }
        [Display(Name = "Country", ResourceType = typeof(Resources.Hotel))]
        [Required(ErrorMessage = "País requerido")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        [Display(Name = "Category", ResourceType = typeof(Resources.Hotel))]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<HotelImage> HotelImages { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Resources.Hotel))]
        public bool Active { get; set; }
        [Display(Name = "Creator", ResourceType = typeof(Resources.Hotel))]
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        [Display(Name = "CreatedDate", ResourceType = typeof(Resources.Hotel))]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "LastModifiedDate", ResourceType = typeof(Resources.Hotel))]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastModifiedDate { get; set; }
    }
}