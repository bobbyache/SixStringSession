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
    public class PracticeTimeProgressTests
    {
        [Test]
        public void Ensure_That_All_Properties_Set_When_Initialized()
        {
            var practiceTimeProgress = new PracticeTimeProgress(
                currentTime: 300,
                targetTime: 600,
                weighting: 100
            );

            Assert.That(practiceTimeProgress.Weighting, Is.EqualTo(100));
            Assert.That(practiceTimeProgress.CurrentTime, Is.EqualTo(300));
            Assert.That(practiceTimeProgress.TargetTime, Is.EqualTo(600));
        }

        [Test]
        public void Half_Way_Between_Current_And_Target_Times_Means_Fifty_Percent_Progress()
        {
            var practiceTimeProgress = new PracticeTimeProgress(
                currentTime: 300,
                targetTime: 600,
                weighting: 100
            );

            Assert.That(practiceTimeProgress.CalculateProgress(), Is.EqualTo(50));
        }

        [Test]
        public void Quarter_Way_Between_Current_And_Target_Times_Means_Quarter_Progress()
        {
            var practiceTimeProgress = new PracticeTimeProgress(
                currentTime: 50,
                targetTime: 200,
                weighting: 100
            );

            Assert.That(practiceTimeProgress.CalculateProgress(), Is.EqualTo(25));
        }

        [Test]
        public void No_TargetTimeAlways_Has_100_Percent_Complete()
        {
            var practiceTimeProgress = new PracticeTimeProgress(
                currentTime: 50,
                targetTime: 0,
                weighting: 100
            );

            Assert.That(practiceTimeProgress.CalculateProgress(), Is.EqualTo(100));
        }

        [Test]
        public void When_CurrentTime_Exceeds_TargetTime_Always_Return_100_Percent()
        {
            var practiceTimeProgress = new PracticeTimeProgress(
                currentTime: 50,
                targetTime: 20,
                weighting: 100
            );

            Assert.That(practiceTimeProgress.CalculateProgress(), Is.EqualTo(100));
        }


        [Test]
        public void When_Seconds_Added_CalculateProgress_Works_As_Expected()
        {
            var practiceTimeProgress = new PracticeTimeProgress(
                currentTime: 300,
                targetTime: 600,
                weighting: 100
            );

            var newPracticeTimeProgress = practiceTimeProgress.AddSeconds(150);

            Assert.AreEqual(450, newPracticeTimeProgress.CurrentTime);
            Assert.That(newPracticeTimeProgress.CalculateProgress(), Is.EqualTo(75));
        }

        [Test]
        public void When_Seconds_Subtracted_CalculateProgress_Works_As_Expected()
        {
            var practiceTimeProgress = new PracticeTimeProgress(
                currentTime: 300,
                targetTime: 600,
                weighting: 100
            );

            var newPracticeTimeProgress = practiceTimeProgress.SubstractSeconds(150);

            Assert.AreEqual(150, newPracticeTimeProgress.CurrentTime);
            Assert.That(newPracticeTimeProgress.CalculateProgress(), Is.EqualTo(25));
        }

        [Test]
        public void When_Minutes_Added_CalculateProgress_Works_As_Expected()
        {
            var practiceTimeProgress = new PracticeTimeProgress(
                currentTime: 300,
                targetTime: 600,
                weighting: 100
            );

            var newPracticeTimeProgress = practiceTimeProgress.AddMinutes(5);

            Assert.AreEqual(600, newPracticeTimeProgress.CurrentTime);
            Assert.That(newPracticeTimeProgress.CalculateProgress(), Is.EqualTo(100));
        }

        [Test]
        public void When_Minutes_Subtracted_CalculateProgress_Works_As_Expected()
        {
            var practiceTimeProgress = new PracticeTimeProgress(
                currentTime: 300,
                targetTime: 600,
                weighting: 100
            );

            var newPracticeTimeProgress = practiceTimeProgress.SubtractMinutes(1);

            Assert.AreEqual(240, newPracticeTimeProgress.CurrentTime);
            Assert.That(newPracticeTimeProgress.CalculateProgress(), Is.EqualTo(40));
        }
    }
}
