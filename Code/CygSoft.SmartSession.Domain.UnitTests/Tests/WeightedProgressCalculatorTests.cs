using CygSoft.SmartSession.Domain.Tasks;
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
        public void WeightinProgressCalculator_CalculateTotalProgress_Test_1()
        {
            var calculator = new ProgressCalculator();

            calculator.Add(new WeightedObj(100) { Weighting = 6000 });
            calculator.Add(new WeightedObj(60) { Weighting = 12000 });
            calculator.Add(new WeightedObj(25) { Weighting = 6000 });

            var progress = calculator.CalculateTotalProgress();

            Assert.That(progress, Is.EqualTo(61.25));
        }

        [Test]
        public void WeightinProgressCalculator_CalculateTotalProgress_Test_2()
        {
            var calculator = new ProgressCalculator();

            calculator.Add(new WeightedObj(100) { Weighting = 25 });
            calculator.Add(new WeightedObj(60) { Weighting = 40 });
            calculator.Add(new WeightedObj(40) { Weighting = 60 });
            calculator.Add(new WeightedObj(25) { Weighting = 75 });

            var progress = calculator.CalculateTotalProgress();

            Assert.That(progress, Is.EqualTo(45.875));
        }

        [Test]
        public void WeightinProgressCalculator_CalculateTotalProgress_Test_3()
        {
            var calculator = new ProgressCalculator();

            calculator.Add(new WeightedObj(100) { Weighting = 25 });
            calculator.Add(new WeightedObj(100) { Weighting = 40 });
            calculator.Add(new WeightedObj(100) { Weighting = 60 });
            calculator.Add(new WeightedObj(100) { Weighting = 40 });
            calculator.Add(new WeightedObj(100) { Weighting = 60 });
            calculator.Add(new WeightedObj(100) { Weighting = 40 });
            calculator.Add(new WeightedObj(100) { Weighting = 60 });
            calculator.Add(new WeightedObj(100) { Weighting = 40 });
            calculator.Add(new WeightedObj(100) { Weighting = 60 });
            calculator.Add(new WeightedObj(100) { Weighting = 75 });

            var progress = calculator.CalculateTotalProgress();

            Assert.That(progress, Is.EqualTo(100));
        }

        public class WeightedObj : IWeightedEntity
        {
            public WeightedObj(double percentComplete)
            {
                PercentCompleted = percentComplete;
            }
            public int Weighting { get; set; }

            public string InstanceId { get; } = Guid.NewGuid().ToString();

            public double PercentCompleted { get; private set; }

            public event EventHandler WeightingChanged;
        }
    }
}
