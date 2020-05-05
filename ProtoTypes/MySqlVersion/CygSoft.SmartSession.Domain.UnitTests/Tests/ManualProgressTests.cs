using CygSoft.SmartSession.Domain.Recording;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class ManualProgressTests
    {
        [Test]
        public void Ensure_That_All_Properties_Set_When_Initialized()
        {
            var manualProgress = new ManualProgress(
                value: 50,
                weighting: 100
            );

            Assert.That(manualProgress.Weighting, Is.EqualTo(100));
            Assert.That(manualProgress.Value, Is.EqualTo(50));
        }

        [Test]
        public void Half_Way_Between_Current_And_Target_Times_Means_Fifty_Percent_Progress()
        {
            var manualProgress = new ManualProgress(
                value: 50,
                weighting: 100
            );

            Assert.That(manualProgress.CalculateProgress(), Is.EqualTo(50));
        }

        [Test]
        public void Quarter_Way_Between_Current_And_Target_Times_Means_Quarter_Progress()
        {
            var manualProgress = new ManualProgress(
                value: 25,
                weighting: 100
            );

            Assert.That(manualProgress.CalculateProgress(), Is.EqualTo(25));
        }

        [Test]
        public void When_Value_Added_CalculateProgress_Works_As_Expected()
        {
            var manualProgress = new ManualProgress(
                value: 50,
                weighting: 100
            );

            var newManualProgress = manualProgress.Increase(10);

            Assert.That(newManualProgress.CalculateProgress(), Is.EqualTo(60));
        }

        [Test]
        public void When_Value_Subtracted_CalculateProgress_Works_As_Expected()
        {
            var manualProgress = new ManualProgress(
                value: 50,
                weighting: 100
            );

            var newManualProgress = manualProgress.Decrease(10); manualProgress.Decrease(10);

            Assert.That(newManualProgress.CalculateProgress(), Is.EqualTo(40));
        }
    }
}
