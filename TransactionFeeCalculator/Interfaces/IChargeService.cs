using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionFeeCalculator.Interfaces
{
    public interface IChargeService
    {
        Task<int> CalculateCharge(string chargeConfigFilePath, int amount);
    }
}
