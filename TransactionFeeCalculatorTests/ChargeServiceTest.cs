using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionFeeCalculator.Implementation;
using Xunit;

namespace TransactionFeeCalculatorTests
{
    public class ChargeServiceTest
    {
     
        public ChargeServiceTest()
        {
        }

        [Fact]
        public async Task CalculateCharge_WhenCalled_ValidPath_ReturnsFeeForTransactionAmount()
        {
           
            var sut = new ChargeService();

            var execPath = AppDomain.CurrentDomain.BaseDirectory;

            var contentRootPath = execPath.Substring(0, execPath.Length - 18);

            var path = Path.Combine(contentRootPath, "Files/fees.config.json");


            var fee = await sut.CalculateCharge(path, 5000);



            Assert.Equal(10, fee);
        }


        [Fact]
        public async Task CalculateCharge_WhenCalled_InvalidPath_ThrowsFileNotFoundException()
        {

            var sut = new ChargeService();

            var execPath = AppDomain.CurrentDomain.BaseDirectory;

            var contentRootPath = execPath.Substring(0, execPath.Length - 18);

            var path = Path.Combine(contentRootPath, "Files/fee.config.json");


            await Assert.ThrowsAnyAsync<FileNotFoundException>(async () => { await sut.CalculateCharge(path, 50030); });
        }
    }
}
