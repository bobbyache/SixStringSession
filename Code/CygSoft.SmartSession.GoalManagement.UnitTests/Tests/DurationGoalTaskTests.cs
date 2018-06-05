using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement.UnitTests.Tests
{
    [TestFixture]
    public class DurationGoalTaskTests
    {
        [Test]
        public void New_DurationGoalTask_StartDate_IsNull()
        {
            DurationGoalTask task = new DurationGoalTask("Task 1");
            Assert.That(task.StartDate, Is.Null);
        }

        [Test]
        public void New_DurationGoalTask_CreateDate_IsSet()
        {
            DurationGoalTask task = new DurationGoalTask("Task 1");
            Assert.That(task.CreateDate, Is.Not.EqualTo(DateTime.MinValue));
        }

        [Test]
        public void New_DurationGoalTask_Has_0_MinutesPracticed()
        {
            DurationGoalTask task = new DurationGoalTask("Task 1");
            Assert.That(task.MinutesPracticed, Is.EqualTo(0));
        }

        [Test]
        public void New_DurationGoalTask_Has_0_PercentCompleted()
        {
            DurationGoalTask task = new DurationGoalTask("Task 1");
            Assert.That(task.PercentCompleted, Is.EqualTo(0));
        }

        [Test]
        public void New_DurationGoalTask_Has_0_EffortWeighting()
        {
            DurationGoalTask task = new DurationGoalTask("Task 1");
            Assert.That(task.Weighting, Is.EqualTo(0));
        }

        [Test]
        public void New_DurationGoalTask_Has_0_TargetUnit()
        {
            DurationGoalTask task = new DurationGoalTask("Task 1");
            Assert.That(task.MinutesPracticed, Is.EqualTo(0));
        }

        [Test]
        public void DurationGoalTask_With_More_Minutes_Than_Target_Returns_100_Percent()
        {
            List<DurationSessionResult> durationSessionResults = new List<DurationSessionResult>()
            {
                new DurationSessionResult(DateTime.Parse("2018/06/22 18:33:20"), 30),
                new DurationSessionResult(DateTime.Parse("2018/06/22 18:33:20"), 100)
            };

            DurationGoalTask task = new DurationGoalTask("Task 1", 100, durationSessionResults);
            Assert.That(task.PercentCompleted, Is.EqualTo(100));
        }

        [Test]
        public void Existing_DurationGoalTask_With_20_Min_Out_Of_60_Min_Is_33_Percent()
        {
            List<DurationSessionResult> durationSessionResults = new List<DurationSessionResult>()
            {
                new DurationSessionResult(DateTime.Parse("2018/06/22 18:33:20"), 5),
                new DurationSessionResult(DateTime.Parse("2018/06/22 18:33:20"), 15)
            };

            DurationGoalTask task = new DurationGoalTask("Task 1", 60, durationSessionResults);
            Assert.That(task.PercentCompleted, Is.InRange(33.3, 33.34));
        }
        [Test]
        public void Existing_DurationGoalTask_With_Adds_Up_SessionResult_Minutes_Correctly()
        {
            List<DurationSessionResult> durationSessionResults = new List<DurationSessionResult>()
            {
                new DurationSessionResult(DateTime.Parse("2018/06/22 18:33:20"), 5),
                new DurationSessionResult(DateTime.Parse("2018/06/22 18:33:20"), 15)
            };


            DurationGoalTask task = new DurationGoalTask("Task 1", 60, durationSessionResults);
            Assert.That(task.MinutesPracticed, Is.EqualTo(20));
        }
    }


}
