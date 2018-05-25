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
            calculator.AddItem("001");
            Assert.AreEqual(100, calculator["001"]);
        }

        [Test]
        public void WeightingCalculator_Add_Second_Item_Will_Be_50_Percent_Weighting()
        {
            WeightingCalculator calculator = new WeightingCalculator();
            calculator.AddItem("001");
            calculator.AddItem("002");
            Assert.AreEqual(50, calculator["001"]);
            Assert.AreEqual(50, calculator["002"]);
        }

        [Test]
        public void WeightingCalculator_Add_Third_Item_Will_Be_33_3_Percent_Weighting()
        {
            WeightingCalculator calculator = new WeightingCalculator();
            calculator.AddItem("001");
            calculator.AddItem("002");
            calculator.AddItem("003");
            //Assert.That(calculator["001"], Is.LessThan(33.4));
            //Assert.That(calculator["001"], Is.LessThan(33.4));
            Assert.That(calculator["001"], Is.InRange(33.3, 33.34));
            Assert.That(calculator["002"], Is.InRange(33.3, 33.34));
            Assert.That(calculator["003"], Is.InRange(33.3, 33.34));

            //Assert.That(calculator["001"], Is.AtLeast(33.3)
            //Assert.AreEqual(33.3, calculator["001"]);
            //Assert.AreEqual(33.3, calculator["002"]);
            //Assert.AreEqual(33.3, calculator["003"]);
        }
    }
}
