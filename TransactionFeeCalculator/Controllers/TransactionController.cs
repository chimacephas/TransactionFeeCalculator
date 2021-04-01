using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using TransactionFeeCalculator.Models;
using TransactionFeeCalculator.Interfaces;

namespace TransactionFeeCalculator.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IChargeService _chargeService;

        public TransactionController(IWebHostEnvironment hostEnvironment,
            IChargeService chargeService)
        {
            _hostEnvironment = hostEnvironment;
            _chargeService = chargeService;
        }


        [HttpPost("processpayment")]
        public async Task<IActionResult> ProcessPayment([FromBody] TransactionDto  dto)
        {

            var path = Path.Combine(_hostEnvironment.WebRootPath, "fees.config.json");
                
            var charge = await _chargeService.CalculateCharge(path, dto.Amount);

            return Ok(charge);
        }
    }
}
