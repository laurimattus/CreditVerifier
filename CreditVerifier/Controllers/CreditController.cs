using CreditVerifier.Models;
using CreditVerifier.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditVerifier.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditController : ControllerBase
    {
        private ICreditService _creditService;

        public CreditController(ICreditService creditService)
        {
            _creditService = creditService;
        }

        /// <summary>Verifies if credit can be obtained and interest rate</summary>
        /// <param name="amount">Credit amount</param>
        /// <param name="existingAmount">Existing credit amount</param>
        /// <param name="months">For how many months is the credit</param>
        [HttpGet("Verify")]
        public ActionResult<CreditResponse> Verify(int amount, double existingAmount, int months)
        {
            if(amount <= 0)
            {
                return StatusCode(500, "Negative or 0 amounts are not allowed");
            }
          
            if (existingAmount <= 0)
            {
                return StatusCode(500, "Negative or 0 existing amounts are not allowed");
            }

            if (amount + existingAmount > 1000000000)
            {
                return StatusCode(500, "Maximum amount and existing amount sum is 1 000 000 000");
            }

            if (months <= 0)
            {
                return StatusCode(500, "Negative or 0 terms are not allowed");
            }
            return Ok(_creditService.Verify(amount, existingAmount, months));
        }
    }
}
