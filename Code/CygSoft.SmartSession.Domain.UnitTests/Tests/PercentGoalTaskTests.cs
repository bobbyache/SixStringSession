using CygSoft.SmartSession.Domain.Sessions;
using CygSoft.SmartSession.Domain.Tasks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class PercentGoalTaskTests
    {
        [Test]
        public void New_PercentGoalTask_StartDate_IsNull()
        {
            var task = new PercentGoalTask();
            task.Title = "Task 1";
            Assert.That(task.StartDate, Is.Null);
        }

        [Test]
        public void New_PercentGoalTask_CreateDate_IsSet()
        {
            var task = new PercentGoalTask();
            task.Title = "Task 1";
            Assert.That(task.CreateDate, Is.Not.EqualTo(DateTime.MinValue));
        }

        [Test]
        public void New_PercentGoalTask_Has_0_MinutesPracticed()
        {
            var task = new PercentGoalTask();
            task.Title = "Task 1";
            Assert.That(task.MinutesPracticed, Is.EqualTo(0));
        }

        [Test]
        public void New_PercentGoalTask_Has_0_PercentCompleted()
        {
            var task = new PercentGoalTask();
            task.Title = "Task 1";
            Assert.That(task.PercentCompleted, Is.EqualTo(0));
        }

        [Test]
        public void New_PercentGoalTask_Has_0_EffortWeighting()
        {
            var task = new PercentGoalTask();
            task.Title = "Task 1";
            Assert.That(task.Weighting, Is.EqualTo(0));
        }

        [Test]
        public void Existing_PercentGoalTask_With_Adds_Up_SessionResult_Minutes_Correctly()
        {
            List<PercentSessionResult> results = new List<PercentSessionResult>()
            {
                new PercentSessionResult(DateTime.Parse("2018/06/22 18:00:00"), DateTime.Parse("2018/06/22 18:05:00"), 40),
                new PercentSessionResult(DateTime.Parse("2018/06/22 18:10:00"), DateTime.Parse("2018/06/22 18:15:00"), 50)
            };

            var task = new PercentGoalTask();
            task.Title = "Task 1";
            task.CreateDate = DateTime.Parse("2018/06/18 18:33:20");
            Assert.That(task.MinutesPracticed, Is.EqualTo(10));
            Assert.Fail(); // results must be assigned first to test this properly
        }

        [Test]
        public void Existing_PercentGoalTask_Presents_Last_Percent_Completed_Value()
        {
            List<PercentSessionResult> results = new List<PercentSessionResult>()
            {
                new PercentSessionResult(DateTime.Parse("2018/06/22 18:00:00"), DateTime.Parse("2018/06/22 18:05:00"), 40),
                new PercentSessionResult(DateTime.Parse("2018/06/22 18:10:00"), DateTime.Parse("2018/06/22 18:15:00"), 50)
            };

            var task = new PercentGoalTask();
            task.Title = "Task 1";
            task.CreateDate = DateTime.Parse("2018/06/18 18:33:20");
            Assert.That(task.PercentCompleted, Is.EqualTo(50));
            Assert.Fail(); // results must be assigned first to test this properly
        }

        [Test]
        public void Existing_PercentGoalTask_Presents_Last_Percent_Completed_Value_Shuffled()
        {
            List<PercentSessionResult> results = new List<PercentSessionResult>()
            {
                new PercentSessionResult(DateTime.Parse("2018/06/22 18:00:00"), DateTime.Parse("2018/06/22 18:05:00"), 70),
                new PercentSessionResult(DateTime.Parse("2018/06/22 18:10:00"), DateTime.Parse("2018/06/22 18:15:00"), 55),
                new PercentSessionResult(DateTime.Parse("2018/06/22 18:20:00"), DateTime.Parse("2018/06/22 18:25:00"), 50)
            };

            var task = new PercentGoalTask();
            task.Title = "Task 1";
            task.CreateDate = DateTime.Parse("2018/06/18 18:33:20");
            Assert.That(task.PercentCompleted, Is.EqualTo(70));
            Assert.Fail(); // results must be assigned first to test this properly
        }

        [Test]
        public void PercentSessionResult_Given_Value_Above_100_Throws_Exception()
        {
            //TestDelegate proc = () => new PercentSessionResult(DateTime.Parse("2018/06/22 18:00:00"), DateTime.Parse("2018/06/22 18:05:00"), 101);
            //Assert.That(proc, Throws.TypeOf<ArgumentOutOfRangeException>());
            Assert.Fail(); // how best to implement this.
        }

        [Test]
        public void PercentSessionResult_Given_Value_Below_0_Throws_Exception()
        {
            //TestDelegate proc = () => new PercentSessionResult(DateTime.Parse("2018/06/22 18:00:00"), DateTime.Parse("2018/06/22 18:05:00"), -1);
            //Assert.That(proc, Throws.TypeOf<ArgumentOutOfRangeException>());
            Assert.Fail(); // how best to implement this.
        }

        [Test]
        public void Existing_PercentGoalTask_Supplies_Correct_StartDate_From_SessionList()
        {
            DateTime expectedStartTime = DateTime.Parse("2018/06/22 18:00:00");

            List<PercentSessionResult> results = new List<PercentSessionResult>()
            {
                new PercentSessionResult(DateTime.Parse("2018/06/22 18:00:00"), DateTime.Parse("2018/06/22 18:05:00"), 70),
                new PercentSessionResult(DateTime.Parse("2018/06/22 18:10:00"), DateTime.Parse("2018/06/22 18:15:00"), 55)
            };


            var task = new PercentGoalTask();
            task.Title = "Task 1";
            task.CreateDate = DateTime.Parse("2018/03/18 18:01:20");
            Assert.That(task.StartDate, Is.EqualTo(expectedStartTime));
            Assert.Fail(); // results must be assigned first to test this properly
        }

        [Test]
        public void Existing_PercentGoalTask_Raises_WeightingChangedEvent_When_Registered()
        {
            var called = false;

            var task = new PercentGoalTask();
            task.Title = "Task 1";
            task.WeightingChanged += (sender, args) => called = true;
            task.Weighting = 42;

            Assert.IsTrue(called);
        }
    }
}
