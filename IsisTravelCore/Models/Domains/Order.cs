using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsisTravelCore.Models.Domains
{
    public class Order
    {

        public int Id { get; set; }
        [Display(Name = "OrderNumber", ResourceType = typeof(Resources.Order))]
        public string OrderNumber { get; set; }
        [Display(Name = "Name", ResourceType = typeof(Resources.Order))]
        public string Name { get; set; }
        [Display(Name = "City", ResourceType = typeof(Resources.Order))]
        public string City { get; set; }
        [Display(Name = "Address", ResourceType = typeof(Resources.Order))]
        public string Address { get;  set; }
        [Display(Name = "CP", ResourceType = typeof(Resources.Order))]
        public string CP { get;  set; }
        [Display(Name = "Country", ResourceType = typeof(Resources.Order))]
        public string Country { get;  set; }
        [Display(Name = "NIE", ResourceType = typeof(Resources.Order))]
        public string NIE { get;  set; }
        [Display(Name = "Phone", ResourceType = typeof(Resources.Order))]
        public string Phone { get;  set; }
        [Display(Name = "OrderDate", ResourceType = typeof(Resources.Order))]
        public DateTime OrderDate { get; set; }
        [Display(Name = "ItemName", ResourceType = typeof(Resources.Order))]
        public string ItemName1 { get; set; }
        [Display(Name = "Quantity", ResourceType = typeof(Resources.Order))]
        public int? Quantity1 { get; set; }
        [Display(Name = "Price", ResourceType = typeof(Resources.Order))]
        public decimal? Price1 { get; set; }
        [Display(Name = "Discount", ResourceType = typeof(Resources.Order))]
        public decimal? Discount1 { get; set; }

        [Display(Name = "ItemName", ResourceType = typeof(Resources.Order))]
        public string ItemName2 { get; set; }
        [Display(Name = "Quantity", ResourceType = typeof(Resources.Order))]
        public int? Quantity2 { get; set; }
        [Display(Name = "Price", ResourceType = typeof(Resources.Order))]
        public decimal? Price2 { get; set; }
        [Display(Name = "Discount", ResourceType = typeof(Resources.Order))]
        public decimal? Discount2 { get; set; }

        [Display(Name = "ItemName", ResourceType = typeof(Resources.Order))]
        public string ItemName3 { get; set; }
        [Display(Name = "Quantity", ResourceType = typeof(Resources.Order))]
        public int? Quantity3 { get; set; }
        [Display(Name = "Price", ResourceType = typeof(Resources.Order))]
        public decimal? Price3 { get; set; }
        [Display(Name = "Discount", ResourceType = typeof(Resources.Order))]
        public decimal? Discount3 { get; set; }

        [Display(Name = "ItemName", ResourceType = typeof(Resources.Order))]
        public string ItemName4 { get; set; }
        [Display(Name = "Quantity", ResourceType = typeof(Resources.Order))]
        public int? Quantity4 { get; set; }
        [Display(Name = "Price", ResourceType = typeof(Resources.Order))]
        public decimal? Price4 { get; set; }
        [Display(Name = "Discount", ResourceType = typeof(Resources.Order))]
        public decimal? Discount4 { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Resources.Category))]
        public bool Active { get; set; }
        [Display(Name = "Creator", ResourceType = typeof(Resources.Category))]
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "CreatedDate", ResourceType = typeof(Resources.Order))]
        public DateTime CreatedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "LastModifiedDate", ResourceType = typeof(Resources.Category))]
        public DateTime LastModifiedDate { get; set; }
        [Display(Name = "Population", ResourceType = typeof(Resources.Order))]
        public int? Population { get;  set; }
    }
}
