using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditVerifier
{
    public class CreditDecisionOptions
    {
        public const string CreditDecision = "CreditDecision";
        public SortedDictionary<string, string> RangeStartsDecisions { get; set; }
        public SortedDictionary<string, string> RangeStartsInterests { get; set; }
  
    }
}
