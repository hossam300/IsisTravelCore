using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsisTravelCore.Models.Domains
{
    public class RequestOffer
    {
        public int Id { get; set; }
        [Display(Name = "Date", ResourceType = typeof(Resources.RequestOffer))]
        public DateTime Date { get; set; }
        [Display(Name = "City", ResourceType = typeof(Resources.RequestOffer))]
        public string City { get; set; }
        public int RoomType { get; set; }
        public decimal RoomTypeVal { get; set; }
        [Display(Name = "AdultQuantity", ResourceType = typeof(Resources.RequestOffer))]
        public int AdultQuantity { get; set; }
        [Display(Name = "BabyQuantity", ResourceType = typeof(Resources.RequestOffer))]
        public int BabyQuantity { get; set; }
        [Display(Name = "ExtraRoomQuantity", ResourceType = typeof(Resources.RequestOffer))]
        public int ExtraRoomQuantity { get; set; }
        [Display(Name = "TotalAdult", ResourceType = typeof(Resources.RequestOffer))]
        public decimal TotalAdult { get; set; }
        [Display(Name = "TotalBaby", ResourceType = typeof(Resources.RequestOffer))]
        public decimal TotalBaby { get; set; }
        [Display(Name = "TotalExtraRoom", ResourceType = typeof(Resources.RequestOffer))]
        public decimal TotalExtraRoom { get; set; }
        [Display(Name = "Total", ResourceType = typeof(Resources.RequestOffer))]
        public decimal Total { get; set; }
        [Display(Name = "Tour", ResourceType = typeof(Resources.RequestOffer))]
        public int TourId { get; set; }
        [Display(Name = "Tour", ResourceType = typeof(Resources.RequestOffer))]
        public Tour Tour { get; set; }
        [Display(Name = "Category", ResourceType = typeof(Resources.RequestOffer))]
        public int CategoryId { get; set; }
        [Display(Name = "Category", ResourceType = typeof(Resources.RequestOffer))]
        public Category Category { get; set; }
        [Display(Name = "Finished", ResourceType = typeof(Resources.RequestOffer))]
        public bool Finished { get; set; }
        [Display(Name = "State", ResourceType = typeof(Resources.RequestOffer))]
        public int State { get; set; }
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
