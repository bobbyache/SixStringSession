using NUnit.Framework;
using System;
using CygSoft.SmartSession.Domain.Tasks;
using System.Collections.Generic;
using CygSoft.SmartSession.Domain.Sessions;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class DurationGoalTaskTests
    {
        [Test]
        public void One_DurationGoalTask_Equals_Another_DurationGoalTask_Correctly()
        {
            var task = new DurationGoalTask();
            task.Id = 23;
            task.Title = "Hello World";

            var anotherTask = new DurationGoalTask();
            anotherTask.Id = 23;
            anotherTask.Title = "Goodbye World";

            Assert.That(task, Is.EqualTo(anotherTask));
        }

        [Test]
        public void New_DurationGoalTask_StartDate_IsNull()
        {
            var task = new DurationGoalTask();
            task.Title = "Task 1";
            task.TargetMinutes = 100;
            Assert.That(task.StartDate, Is.Null);
        }

        [Test]
        public void New_DurationGoalTask_CreateDate_IsSet()
        {
            var task = new DurationGoalTask();
            task.Title = "Task 1";
            task.TargetMinutes = 100;
            Assert.That(task.CreateDate, Is.Not.EqualTo(DateTime.MinValue));
        }

        [Test]
        public void New_DurationGoalTask_Has_0_MinutesPracticed()
        {
            var task = new DurationGoalTask();
            task.Title = "Task 1";
            task.TargetMinutes = 100;
            Assert.That(task.MinutesPracticed, Is.EqualTo(0));
        }

        [Test]
        public void New_DurationGoalTask_Has_0_PercentCompleted()
        {
            var task = new DurationGoalTask();
            task.Title = "Task 1";
            task.TargetMinutes = 100;
            Assert.That(task.PercentCompleted, Is.EqualTo(0));
        }

        [Test]
        public void New_DurationGoalTask_Has_0_EffortWeighting()
        {
            var task = new DurationGoalTask();
            task.Title = "Task 1";
            task.TargetMinutes = 100;
            Assert.That(task.Weighting, Is.EqualTo(0));
        }

        [Test]
        public void New_DurationGoalTask_Has_0_TargetUnit()
        {
            var task = new DurationGoalTask();
            task.Title = "Task 1";
            task.TargetMinutes = 100;
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

            var task = new DurationGoalTask();
            task.Title = "Task 1";
            task.TargetMinutes = 100;
            task.AddSessionRange(results);

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

            var task = new DurationGoalTask();
            task.Title = "Task 1";
            task.TargetMinutes = 60;
            task.AddSessionRange(results);
            Assert.That(task.PercentCompleted, Is.InRange(33.3, 33.34));
        }
        [Test]
        public void Existing_DurationGoalTask_Adds_Up_SessionResult_Minutes_Correctly()
        {
            List<DurationSessionResult> results = new List<DurationSessionResult>()
            {
                new DurationSessionResult(DateTime.Parse("2018/06/22 18:00:00"), DateTime.Parse("2018/06/22 18:05:00")),
                new DurationSessionResult(DateTime.Parse("2018/06/22 18:10:00"), DateTime.Parse("2018/06/22 18:25:00"))
            };

            var task = new DurationGoalTask();
            task.Title = "Task 1";
            task.TargetMinutes = 100;
            task.AddSessionRange(results);
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

            List<DurationSessionResult> results = new List<DurationSessionResult>()
            {
                new DurationSessionResult(DateTime.Parse("2018/06/22 18:00:00"), DateTime.Parse("2018/06/22 18:05:00")),
                new DurationSessionResult(DateTime.Parse("2018/06/22 18:10:00"), DateTime.Parse("2018/06/22 18:25:00"))
            };

            var task = new DurationGoalTask();
            task.Title = "Task 1";
            task.TargetMinutes = 100;
            task.AddSessionRange(results);
            Assert.That(task.StartDate, Is.EqualTo(expectedStartTime));
        }

        [Test]
        public void Existing_DurationGoalTask_Raises_WeightingChangedEvent_When_Registered()
        {
            var called = false;

            var task = new DurationGoalTask();
            task.Title = "Task 1";
            task.TargetMinutes = 100;
            task.WeightingChanged += (sender, args) => called = true;
            task.Weighting = 42;

            Assert.IsTrue(called);
        }
    }


}
