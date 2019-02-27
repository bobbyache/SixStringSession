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
    public class SpeedProgressTests
    {
        [Test]
        public void Ensure_That_All_Properties_Set_When_Initialized()
        {
            var speedProgress = new SpeedProgress(
                initialSpeed: 10,
                currentSpeed: 60,
                targetSpeed: 110,
                weighting: 100
            );

            Assert.That(speedProgress.Weighting, Is.EqualTo(100));

            Assert.That(speedProgress.InitialSpeed, Is.EqualTo(10));
            Assert.That(speedProgress.CurrentSpeed, Is.EqualTo(60));
            Assert.That(speedProgress.TargetSpeed, Is.EqualTo(110));
        }

        [Test]
        public void Half_Way_Between_Initial_And_Target_Speeds_Means_Fifty_Percent_Progress()
        {
            var speedProgress = new SpeedProgress(
                initialSpeed: 10,
                currentSpeed: 60,
                targetSpeed: 110,
                weighting: 100
            );

            Assert.That(speedProgress.CalculateProgress(), Is.EqualTo(50));
        }

        [Test]
        public void Quarter_Way_Between_Initial_And_Target_Speeds_Means_Quarter_Progress()
        {
            var speedProgress = new SpeedProgress(
                initialSpeed: 10,
                currentSpeed: 35,
                targetSpeed: 110,
                weighting: 100
            );

            Assert.That(speedProgress.CalculateProgress(), Is.EqualTo(25));
        }

        [Test]
        public void No_InitialSpeed_But_Has_TargetSpeed_Calcs_Correctly()
        {
            var speedProgress = new SpeedProgress(
                initialSpeed: 0,
                currentSpeed: 35,
                targetSpeed: 100,
                weighting: 100
            );

            Assert.That(speedProgress.CalculateProgress(), Is.EqualTo(35));
        }

        [Test]
        public void No_TargetSpeed_Always_Has_100_Percent_Complete()
        {
            var speedProgress = new SpeedProgress(
                initialSpeed: 60,
                currentSpeed: 80,
                targetSpeed: 0,
                weighting: 100
            );

            Assert.That(speedProgress.CalculateProgress(), Is.EqualTo(100));
        }

        [Test]
        public void When_CurrentSpeed_Exceeds_TargetSpeed_Always_Return_100_Percent()
        {
            var speedProgress = new SpeedProgress(
                initialSpeed: 60,
                currentSpeed: 120,
                targetSpeed: 100,
                weighting: 100
            );

            Assert.That(speedProgress.CalculateProgress(), Is.EqualTo(100));
        }

        [Test]
        public void When_CurrentSpeed_IsBelow_InitialSpeed_Always_Return_0_Percent()
        {
            var speedProgress = new SpeedProgress(
                initialSpeed: 60,
                currentSpeed: 50,
                targetSpeed: 100,
                weighting: 100
            );

            Assert.That(speedProgress.CalculateProgress(), Is.EqualTo(0));
        }

        [Test]
        public void When_Ticks_Added_CalculateProgress_Works_As_Expected()
        {
            var speedProgress = new SpeedProgress(
                initialSpeed: 100,
                currentSpeed: 125,
                targetSpeed: 200,
                weighting: 100
            );

            var newSpeedProgress = speedProgress.AddTicks(25);

            Assert.AreEqual(150, newSpeedProgress.CurrentSpeed);
            Assert.That(newSpeedProgress.CalculateProgress(), Is.EqualTo(50));
        }

        [Test]
        public void When_Ticks_OverTargetSpeed_Added_CalculateProgress_Works_As_Expected()
        {
            var speedProgress = new SpeedProgress(
                initialSpeed: 100,
                currentSpeed: 125,
                targetSpeed: 200,
                weighting: 100
            );

            var newSpeedProgress = speedProgress.AddTicks(100);

            Assert.That(newSpeedProgress.CalculateProgress(), Is.EqualTo(100));
        }

        [Test]
        public void When_Ticks_Subtracted_CalculateProgress_Works_As_Expected()
        {
            var speedProgress = new SpeedProgress(
                initialSpeed: 100,
                currentSpeed: 150,
                targetSpeed: 200,
                weighting: 100
            );

            var newSpeedProgress = speedProgress.SubtractTicks(25);

            Assert.AreEqual(125, newSpeedProgress.CurrentSpeed);
            Assert.That(newSpeedProgress.CalculateProgress(), Is.EqualTo(25));
        }

        [Test]
        public void When_Ticks_Subtracted_GoBelowZero_CalculateProgress_Works_As_Expected()
        {
            var speedProgress = new SpeedProgress(
                initialSpeed: 100,
                currentSpeed: 125,
                targetSpeed: 200,
                weighting: 100
            );

            var newSpeedProgress = speedProgress.SubtractTicks(150);

            Assert.That(newSpeedProgress.CalculateProgress(), Is.EqualTo(0));
        }
    }
}
