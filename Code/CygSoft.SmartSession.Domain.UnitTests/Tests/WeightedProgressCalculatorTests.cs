using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Infrastructure;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class WeightedProgressCalculatorTests
    {
        [Test]
        public void WeightedProgressCalculator_CalculateTotalProgress_Test_1()
        {
            var calculator = new WeightedProgressCalculator();

            calculator.Add(new WeightedObj(100, 6000));
            calculator.Add(new WeightedObj(60, 12000));
            calculator.Add(new WeightedObj(25, 6000));

            var progress = calculator.CalculateTotalProgress();

            Assert.That(progress, Is.EqualTo(61.25));
        }

        [Test]
        public void WeightedProgressCalculator_CalculateTotalProgress_Test_2()
        {
            var calculator = new WeightedProgressCalculator();

            calculator.Add(new WeightedObj(100, 25));
            calculator.Add(new WeightedObj(60, 40));
            calculator.Add(new WeightedObj(40, 60));
            calculator.Add(new WeightedObj(25, 75));

            var progress = calculator.CalculateTotalProgress();

            Assert.That(progress, Is.EqualTo(45.875));
        }

        [Test]
        public void WeightedProgressCalculator_CalculateTotalProgress_Test_3()
        {
            var calculator = new WeightedProgressCalculator();

            calculator.Add(new WeightedObj(100, 25));
            calculator.Add(new WeightedObj(100, 40));
            calculator.Add(new WeightedObj(100, 60));
            calculator.Add(new WeightedObj(100, 40));
            calculator.Add(new WeightedObj(100, 60));
            calculator.Add(new WeightedObj(100, 40));
            calculator.Add(new WeightedObj(100, 60));
            calculator.Add(new WeightedObj(100, 40));
            calculator.Add(new WeightedObj(100, 60));
            calculator.Add(new WeightedObj(100, 75));


            var progress = calculator.CalculateTotalProgress();

            Assert.That(progress, Is.EqualTo(100));
        }

        public class WeightedObj : IWeightedEntity
        {
            private readonly double percentCompleted;

            public WeightedObj(double percentComplete, int weighting)
            {
                percentCompleted = percentComplete;
                Weighting = weighting;
            }
            public int Weighting { get; private set; }

            public double PercentCompleted()
            {
                return percentCompleted;
            }
        }
    }
}
