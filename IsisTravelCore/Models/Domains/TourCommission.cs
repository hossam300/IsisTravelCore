using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsisTravelCore.Models.Domains
{
    public class TourCommission
    {
        public int Id { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal TaxValue { get; set; }
        public decimal CommissionPercentage { get; set; }
        public decimal CommissionTax { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
