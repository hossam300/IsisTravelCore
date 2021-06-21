using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsisTravelCore.Models.Domains
{
    public class Subscriber
    {
        public int Id { get; set; }
        [Display(Name = "Email", ResourceType = typeof(Resources.Category))]
        public string Email { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Resources.Category))]
        public bool Active { get; set; }
      
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "CreatedDate", ResourceType = typeof(Resources.Category))]
        public DateTime CreatedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "LastModifiedDate", ResourceType = typeof(Resources.Category))]
        public DateTime LastModifiedDate { get; set; }
    }
}
