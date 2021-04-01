using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TransactionFeeCalculator.Interfaces;
using TransactionFeeCalculator.Models;

namespace TransactionFeeCalculator.Implementation
{
    public class ChargeService : IChargeService
    {
        public async Task<int> CalculateCharge(string chargeConfigFilePath,int amount)
        {

            var cofigFIle = await System.IO.File.ReadAllTextAsync(chargeConfigFilePath);


            var amountConfig = JsonSerializer.Deserialize<ChargeConfiguration>(cofigFIle);


            int calculatedFee = 0;

            foreach (var fee in amountConfig.fees)
            {
                if (amount >= fee.minAmount && amount <= fee.maxAmount)
                {
                    calculatedFee = fee.feeAmount;
                    break;
                }
                   
            }

            return calculatedFee;
        }
    }
}
