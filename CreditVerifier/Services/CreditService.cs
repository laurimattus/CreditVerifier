using CreditVerifier.Models;
using CreditVerifier.Tools;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Range = CreditVerifier.Tools.Range;

namespace CreditVerifier.Services
{
    public class CreditService : ICreditService
    {
        CreditDecisionOptions _options;
        List<Range> _amountRanges;
        List<Range> _interestRanges;

        public CreditService(IOptions<CreditDecisionOptions> options)
        {
            _options = options.Value;
            _amountRanges = RangeTool.StartPositionsToRanges(_options.RangeStartsDecisions.Keys.Select(x => int.Parse(x)).ToArray());
            _interestRanges = RangeTool.StartPositionsToRanges(_options.RangeStartsInterests.Keys.Select(x => int.Parse(x)).ToArray());

        }
        public CreditResponse Verify(int amount, double existingAmount, int months)
        {
            var creditResponse = new CreditResponse();

            creditResponse.Decision = GetDecision(amount);
            if (creditResponse.Decision == Decision.No)
                return creditResponse;

            var totalAmount = existingAmount + amount;
            var endAmount = CalculateEndAmount(totalAmount, months);
            creditResponse.InterestRate = GetInterestRate(endAmount);
            return creditResponse;
        }

        private double GetInterestRate(double amount)
        {
            var amountRangeIndex = RangeTool.FindRangeIndex(_interestRanges, amount);
            return double.Parse(_options.RangeStartsInterests.Values.ElementAt(amountRangeIndex));
        }

        private string GetDecision(double amount)
        {
            var amountRangeIndex = RangeTool.FindRangeIndex(_amountRanges, amount);
            return _options.RangeStartsDecisions.Values.ElementAt(amountRangeIndex);
        }

        public double CalculateEndAmount(double amount, int months)
        {
            int years = (int)Math.Floor((double)months / 12);

            var interestRate = GetInterestRate(amount);
            
            // Compound interest
            var total = amount * Math.Pow(1 + (double)interestRate / 100, years);
            
            var monthsLeft = months - 12 * years;
            total += monthsLeft * interestRate / 12;            

            return Math.Round(total, 2);
        }
    }
}
