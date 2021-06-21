using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsisTravelCore.Models.Domains
{
    public class TourQuote
    {
        public int Id { get; set; }
        [Display(Name = "QuoteName", ResourceType = typeof(Resources.Service))]
        public string QuoteName { get; set; }
        [Display(Name = "QuotePrice", ResourceType = typeof(Resources.Service))]
        public decimal QuotePrice { get; set; }
        [Display(Name = "Tour", ResourceType = typeof(Resources.Service))]
        public int TourId { get; set; }
        public Tour Tour { get; set; }
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
