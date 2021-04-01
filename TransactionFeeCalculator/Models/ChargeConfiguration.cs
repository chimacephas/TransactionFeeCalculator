using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionFeeCalculator.Models
{
    public class Fee
    {
        public int minAmount { get; set; }
        public int maxAmount { get; set; }
        public int feeAmount { get; set; }
    }

    public class ChargeConfiguration
    {
        public List<Fee> fees { get; set; }
    }
}
