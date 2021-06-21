﻿
using System;

namespace IsisTravelCore.Models.Domains
{
    public class TourInclude
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public virtual Tour Tour { get; set; }
        public int IncludeId { get; set; }
        public virtual Include Include { get; set; }
        public bool Active { get; set; }
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}