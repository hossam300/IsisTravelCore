using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IsisTravelCore.Models.Domains
{
    public class IsisPage
    {
        public int Id { get; set; }
        [Display(Name = "Title", ResourceType = typeof(Resources.Service))]
        public string Title { get; set; }
        [Column(TypeName = "text")]
        [Display(Name = "Description", ResourceType = typeof(Resources.Include))]
        public string Description { get; set; }
        public PageCatgory PageCatgory { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Resources.Include))]
        public bool Active { get; set; }
        [Display(Name = "Creator", ResourceType = typeof(Resources.Include))]
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "CreatedDate", ResourceType = typeof(Resources.Include))]
        public DateTime CreatedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "LastModifiedDate", ResourceType = typeof(Resources.Include))]
        public DateTime LastModifiedDate { get; set; }
    }
}
