using CreditVerifier.Models;
using CreditVerifier.Services;
using CreditVerifier.Tools;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Range = CreditVerifier.Tools.Range;

namespace CreditVerifier.Tests
{
    [TestClass]
    public class RangeToolsTests
    {
        IOptions<CreditDecisionOptions> _options;
        ICreditService _creditService;


        [TestInitialize]
        public void Initialize()
        {

            _options = Options.Create<CreditDecisionOptions>(new CreditDecisionOptions()
            {
                RangeStartsDecisions = new()
                {
                    { "0", "No" },
                    { "2000", "Yes" },
                    { "69000", "No" }
                },
                RangeStartsInterests = new()
                {
                    { "0", "2" },
                    { "20000", "3" },
                    { "40000", "4" }
                },
            });

            _creditService = new CreditService(_options);
        }

        [TestMethod]
        public void GetInterestRate_IfAmountAndMonthsPositive_ShouldReturnCorrectInterestRate()
        {
            // Act
            var result = _creditService.CalculateEndAmount(1000, 40);

            // Assert
            result.Should().Be(1061.87);
        }

        [TestMethod]
        public void Verify_PositiveAmountAndExistingAmountAndMonths_ShouldReturnCorrectInterestRateAndDecision()
        {
            // Act
            var result = _creditService.Verify(2000, 1000, 12);

            // Assert
            result.Decision.Should().Be(Decision.Yes);
            result.InterestRate.Should().Be(2);
        }
    }
}