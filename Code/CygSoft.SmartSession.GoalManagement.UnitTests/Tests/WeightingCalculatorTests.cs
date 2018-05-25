using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement.UnitTests.Tests
{
    [TestFixture]
    public class WeightingCalculatorTests
    {
        [Test]
        public void WeightingCalculator_Add_First_Item_Will_Be_100_Percent_Weighting()
        {
            WeightingCalculator calculator = new WeightingCalculator();
            calculator.Update("001", 100);
            Assert.AreEqual(100, calculator["001"]);
        }

        [Test]
        public void WeightingCalculator_Add_Two_Equivalent_Calculates_To_50_Percent_Weighting()
        {
            WeightingCalculator calculator = new WeightingCalculator();
            calculator.Update("001", 100);
            calculator.Update("002", 100); // relatively the same value so will be 50% of the pie.
            Assert.AreEqual(50, calculator["001"]);
            Assert.AreEqual(50, calculator["002"]);
        }

        [Test]
        public void WeightingCalculator_Add_Three_Equivalent_Calculates_To_33_3_Percent_Weighting()
        {
            WeightingCalculator calculator = new WeightingCalculator();
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
            WeightingCalculator calculator = new WeightingCalculator();

            calculator.Update("001", 100);
            calculator.Update("002", 150);
            calculator.Update("003", 170);

            // Percentage calculated as the "relative" weighting by looking at a value between 1 and 1000
            Assert.That(calculator["001"], Is.InRange(23.8, 23.899));
            Assert.That(calculator["002"], Is.InRange(35.7, 35.799));
            Assert.That(calculator["003"], Is.InRange(40.4, 40.499));
        }
    }
}
