using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsisTravelCore.Models.Domains
{
    public class Feature
    {
        public int Id { get; set; }
        [Display(Name = "Tour", ResourceType = typeof(Resources.SildeShow))]
        public int TourId { get; set; }
        [Display(Name = "Tour", ResourceType = typeof(Resources.SildeShow))]
        public Tour Tour { get; set; }
        [Display(Name = "Icon", ResourceType = typeof(Resources.Service))]
        public string Icon { get; set; }
        [Display(Name = "Title", ResourceType = typeof(Resources.Service))]
        public string Title { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Resources.Service))]
        public string Description { get; set; }
    }
}
