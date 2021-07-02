using CreditVerifier.Tools;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Range = CreditVerifier.Tools.Range;

namespace CreditVerifier.Tests
{
    [TestClass]
    public class CreditServiceTests
    {
        List<Range> _ranges;

        [TestInitialize]
        public void Initialize()
        {
            _ranges = new List<Range>()
            {
                new Range(0, 19999),
                new Range(20000, 39999)
            };
        }

        [TestMethod]
        public void CalculateEndAmount_IfRangesIsNull_ShouldReturnMinusOne()
        {
            // Act
            var result = RangeTool.FindRangeIndex(null, 0);

            // Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void FindRangeIndex_IfRangesIsEmpty_ShouldReturnMinusOne()
        {
            // Act
            var result = RangeTool.FindRangeIndex(new List<Range>(), 0);

            // Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-100.25)]
        [DataRow(1000000001)]
        public void FindRangeIndex_IfAmountIsZeroOrNegativeGreaterThanBillion_ShouldReturnMinusOne(double amount)
        {
            // Act
            var result = RangeTool.FindRangeIndex(_ranges, amount);

            // Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(15000)]
        [DataRow(19999)]
        public void FindRangeIndex_IfAmountIsBetween1And19999_ShouldReturn0(double amount)
        {
            // Act
            var result = RangeTool.FindRangeIndex(_ranges, amount);

            // Assert
            result.Should().Be(0);
        }

        [TestMethod]
        [DataRow(20000)]
        [DataRow(30000)]
        [DataRow(39999)]
        public void FindRangeIndex_IfAmountIsBetween2000And39999_ShouldReturn1(double amount)
        {
            // Act
            var result = RangeTool.FindRangeIndex(_ranges, amount);

            // Assert
            result.Should().Be(1);
        }
      
        [TestMethod]
        [DataRow(40000)]
        [DataRow(70000)]
        [DataRow(10000000)]
        public void FindRangeIndex_IfCreaterBetween40000AndBillion_ShouldReturn2(double amount)
        {
            // Act
            var result = RangeTool.FindRangeIndex(_ranges, amount);

            // Assert
            result.Should().Be(2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StartPositionsToRanges_IfStartPositionsIsNull_ShouldThrowException()
        {
            // Act
            RangeTool.StartPositionsToRanges(null);
        }

        [TestMethod]
        public void StartPositionsToRanges_IfStartPositionsIsEmpty_ShouldReturnEmptyList()
        {
            // Act
            var result = RangeTool.StartPositionsToRanges(new int[0]);

            // Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void StartPositionsToRanges_ShouldReturnCorrectList()
        {
            // Act
            var result = RangeTool.StartPositionsToRanges(new int[] { 0, 20000, 40000 });

            // Assert
            result.Should().BeEquivalentTo(_ranges);
        }
    }
}