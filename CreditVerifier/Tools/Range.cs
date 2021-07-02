using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditVerifier.Tools
{
    public class Range
    {
        public int Start { get; set; }
        public int End { get; set; }

        public Range(int start, int end)
        {
            Start = start;
            End = end;
        }
    }
}
