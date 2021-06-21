using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsisTravelCore.Models
{
    public class TourQuoteVM
    {
        public int TourQuoteId { get; set; }
        public string QuoteName { get; set; }
        public decimal QuotePrice { get; set; }
        public int TourId { get; set; }
    }
}
