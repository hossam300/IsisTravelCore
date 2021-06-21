using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsisTravelCore.Models.Domains
{
    public class UserCommission
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal CommissionPercentage { get; set; }
        public decimal CommissionTax { get; set; }
    }
}
