using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement.UnitTests.Tests
{
    [TestFixture]
    [Category("WeightingCalculator")]
    public class WeightingCalculatorTests
    {
        [Test]
        public void WeightingCalculator_Add_First_Item_Will_Be_100_Percent_Weighting()
        {
            WeightingCalculator calculator = new WeightingCalculator(1000);
            calculator.Update("001", 100);
            Assert.AreEqual(100, calculator["001"]);
        }

        [Test]
        public void WeightingCalculator_Add_Two_Equivalent_Calculates_To_50_Percent_Weighting()
        {
            WeightingCalculator calculator = new WeightingCalculator(100);
            calculator.Update("001", 100);
            calculator.Update("002", 100); // relatively the same value so will be 50% of the pie.
            Assert.AreEqual(50, calculator["001"]);
            Assert.AreEqual(50, calculator["002"]);
        }

        [Test]
        public void WeightingCalculator_Add_Three_Equivalent_Calculates_To_33_3_Percent_Weighting()
        {
            WeightingCalculator calculator = new WeightingCalculator(400);
            calculator.Update("001", 100);
            calculator.Update("002", 100);
            calculator.Update("003", 100);  // relatively the same value so will be 50% of the pie.

            Assert.That(calculator["001"], Is.InRange(33.3, 33.34));
            Assert.That(calculator["002"], Is.InRange(33.3, 33.34));
            Assert.That(calculator["003"], Is.InRange(33.3, 33.34));
        }

        [Test]
        public void New_WeightingCalculator_With_Staggered_Values_Calculated_Correctly()
        {
            WeightingCalculator calculator = new WeightingCalculator(500);

            calculator.Update("001", 100);
            calculator.Update("002", 150);
            calculator.Update("003", 170);

            // Percentage calculated as the "relative" weighting by looking at a value between 1 and X max value
            Assert.That(calculator["001"], Is.InRange(23.8, 23.899));
            Assert.That(calculator["002"], Is.InRange(35.7, 35.799));
            Assert.That(calculator["003"], Is.InRange(40.4, 40.499));
        }

        [Test]
        public void New_WeightingCalculator_Enforces_MaxValue_Constraint()
        {
            WeightingCalculator calculator = new WeightingCalculator(1000);

            TestDelegate proc = () => calculator.Update("001", 1001);
            Assert.That(proc, Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void WeightingCalculator_Returns_Correct_Weighted_Percentage_Given_A_Percentage()
        {
            WeightingCalculator calculator = new WeightingCalculator(500);

            calculator.Update("001", 100);
            calculator.Update("002", 100);
            calculator.Update("003", 200);

            // Why? Because this adds up to 50% which we know is the actual weighted progress we'd like to see.
            Assert.That(calculator.GetWeightedPercentage("001", 50), Is.EqualTo(12.5));
            Assert.That(calculator.GetWeightedPercentage("002", 50), Is.EqualTo(12.5));
            Assert.That(calculator.GetWeightedPercentage("003", 50), Is.EqualTo(25));
        }

        [Test]
        public void WeightingCalculator_GetWeightedPercetage_When_Passed_Percentage_Exceeding_100_Throws_Exception()
        {
            WeightingCalculator calculator = new WeightingCalculator(500);
            calculator.Update("001", 100);

            TestDelegate proc = () => calculator.GetWeightedPercentage("001", 1001);
            Assert.That(proc, Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void WeightingCalculator_GetWeightedPercetage_When_Passed_Percentage_LessThan_0_Throws_Exception()
        {
            WeightingCalculator calculator = new WeightingCalculator(500);
            calculator.Update("001", 100);

            TestDelegate proc = () => calculator.GetWeightedPercentage("001", -1);
            Assert.That(proc, Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void WeightingCalculator_GetWeightedPercetage_When_Passed_InvalidId_Throws_Exception()
        {
            WeightingCalculator calculator = new WeightingCalculator(500);
            calculator.Update("001", 100);

            TestDelegate proc = () => calculator.GetWeightedPercentage("002", 80);
            Assert.That(proc, Throws.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}
