using CygSoft.SmartSession.GoalManagement.Sessions;
using CygSoft.SmartSession.GoalManagement.Tasks;
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
            DurationGoalTask task = new DurationGoalTask("Task 1", 100);
            Assert.That(task.StartDate, Is.Null);
        }

        [Test]
        public void New_DurationGoalTask_CreateDate_IsSet()
        {
            DurationGoalTask task = new DurationGoalTask("Task 1", 100);
            Assert.That(task.CreateDate, Is.Not.EqualTo(DateTime.MinValue));
        }

        [Test]
        public void New_DurationGoalTask_Has_0_MinutesPracticed()
        {
            DurationGoalTask task = new DurationGoalTask("Task 1", 100);
            Assert.That(task.MinutesPracticed, Is.EqualTo(0));
        }

        [Test]
        public void New_DurationGoalTask_Has_0_PercentCompleted()
        {
            DurationGoalTask task = new DurationGoalTask("Task 1", 100);
            Assert.That(task.PercentCompleted, Is.EqualTo(0));
        }

        [Test]
        public void New_DurationGoalTask_Has_0_EffortWeighting()
        {
            DurationGoalTask task = new DurationGoalTask("Task 1", 100);
            Assert.That(task.Weighting, Is.EqualTo(0));
        }

        [Test]
        public void New_DurationGoalTask_Has_0_TargetUnit()
        {
            DurationGoalTask task = new DurationGoalTask("Task 1", 100);
            Assert.That(task.MinutesPracticed, Is.EqualTo(0));
        }

        [Test]
        public void DurationGoalTask_With_More_Minutes_Than_Target_Returns_100_Percent()
        {
            List<DurationSessionResult> results = new List<DurationSessionResult>()
            {
                new DurationSessionResult(DateTime.Parse("2018/06/22 18:33:20"), DateTime.Parse("2018/06/22 18:40:20")),
                new DurationSessionResult(DateTime.Parse("2018/06/23 14:33:20"), DateTime.Parse("2018/06/23 18:52:20"))
            };

            DurationGoalTask task = new DurationGoalTask("Task 1", DateTime.Parse("2018/06/18 18:33:20"), 100, results);
            Assert.That(task.PercentCompleted, Is.EqualTo(100));
        }

        [Test]
        public void Existing_DurationGoalTask_With_20_Min_Out_Of_60_Min_Is_33_Percent()
        {
            List<DurationSessionResult> results = new List<DurationSessionResult>()
            {
                new DurationSessionResult(DateTime.Parse("2018/06/22 18:00:00"), DateTime.Parse("2018/06/22 18:05:00")),
                new DurationSessionResult(DateTime.Parse("2018/06/22 18:10:00"), DateTime.Parse("2018/06/22 18:25:00"))
            };

            DurationGoalTask task = new DurationGoalTask("Task 1", DateTime.Parse("2018/06/20 18:33:20"), 60, results);
            Assert.That(task.PercentCompleted, Is.InRange(33.3, 33.34));
        }
        [Test]
        public void Existing_DurationGoalTask_Adds_Up_SessionResult_Minutes_Correctly()
        {
            List<DurationSessionResult> durationSessionResults = new List<DurationSessionResult>()
            {
                new DurationSessionResult(DateTime.Parse("2018/06/22 18:00:00"), DateTime.Parse("2018/06/22 18:05:00")),
                new DurationSessionResult(DateTime.Parse("2018/06/22 18:10:00"), DateTime.Parse("2018/06/22 18:25:00"))
            };


            DurationGoalTask task = new DurationGoalTask("Task 1", DateTime.Parse("2018/06/18 18:33:20"), 60, durationSessionResults);
            Assert.That(task.MinutesPracticed, Is.EqualTo(20));
        }

        [Test]
        public void DurationSessionResult_Given_EndDate_LessThan_StartDate_Throws_Exception()
        {
            TestDelegate proc = () => new DurationSessionResult(DateTime.Parse("2018/06/22 18:33:20"), DateTime.Parse("2018/06/22 18:33:19"));
            Assert.That(proc, Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Existing_DurationGoalTask_Supplies_Correct_StartDate_From_SessionList()
        {
            DateTime expectedStartTime = DateTime.Parse("2018/06/22 18:00:00");

            List<DurationSessionResult> durationSessionResults = new List<DurationSessionResult>()
            {
                new DurationSessionResult(DateTime.Parse("2018/06/22 18:00:00"), DateTime.Parse("2018/06/22 18:05:00")),
                new DurationSessionResult(DateTime.Parse("2018/06/22 18:10:00"), DateTime.Parse("2018/06/22 18:25:00"))
            };


            DurationGoalTask task = new DurationGoalTask("Task 1", DateTime.Parse("2018/03/18 18:01:20"), 60, durationSessionResults);
            Assert.That(task.StartDate, Is.EqualTo(expectedStartTime));
        }

        [Test]
        public void Existing_DurationGoalTask_Raises_WeightingChangedEvent_When_Registered()
        {
            var called = false;

            var task = new DurationGoalTask("Duration Task", 100);
            task.WeightingChanged += (sender, args) => called = true;
            task.Weighting = 42;

            Assert.IsTrue(called);
        }
    }


}
