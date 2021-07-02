using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditVerifier.Models
{
    public class CreditResponse
    {
        public double InterestRate { get; set; }
        public string Decision { get; set; }
    }
}
