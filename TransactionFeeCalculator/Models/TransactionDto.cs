using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionFeeCalculator.Models
{
    public class TransactionDto
    {
        [Range(1, 999999999)]
        public int Amount { get; set; }
    }
}
