﻿using JsonDb;
using SmartSession.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JsonDbTests
{
    public class WeightedProgressTests
    {
        public class WeightedProgressCalculatorTests
        {
            [Fact]
            public void WeightedProgressCalculator_CalculateTotalProgress_Test_X()
            {
                var calculator = new WeightedProgressCalculator();

                calculator.Add(new WeightedObj(100, 0.5));
                calculator.Add(new WeightedObj(50, 0.75));

                var progress = calculator.CalculateTotalProgress();

                Assert.Equal(70, progress);
            }

            [Fact]
            public void WeightedProgressCalculator_CalculateTotalProgress_Test_1()
            {
                var calculator = new WeightedProgressCalculator();

                calculator.Add(new WeightedObj(100, 6000));
                calculator.Add(new WeightedObj(60, 12000));
                calculator.Add(new WeightedObj(25, 6000));

                var progress = calculator.CalculateTotalProgress();

                Assert.Equal(61.25, progress);
            }

            [Fact]
            public void WeightedProgressCalculator_CalculateTotalProgress_Test_2()
            {
                var calculator = new WeightedProgressCalculator();

                calculator.Add(new WeightedObj(100, 25));
                calculator.Add(new WeightedObj(60, 40));
                calculator.Add(new WeightedObj(40, 60));
                calculator.Add(new WeightedObj(25, 75));

                var progress = calculator.CalculateTotalProgress();

                Assert.Equal(45.875, progress);
            }

            [Fact]
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

                Assert.Equal(100, progress);
            }

            public class WeightedObj : IWeightedEntity
            {
                private readonly double percentCompleted;

                public WeightedObj(double percentComplete, double weighting)
                {
                    percentCompleted = percentComplete;
                    Weighting = weighting;
                }
                public double Weighting { get; private set; }

                public double PercentCompleted()
                {
                    return percentCompleted;
                }
            }
        }
    }
}


