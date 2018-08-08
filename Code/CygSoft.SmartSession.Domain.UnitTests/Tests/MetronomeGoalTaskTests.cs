using CygSoft.SmartSession.Domain.Sessions;
using CygSoft.SmartSession.Domain.Tasks;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class MetronomeGoalTaskTests
    {
        [Test]
        public void One_MetronomeGoalTask_Equals_Another_MetronomeGoalTask_Correctly()
        {
            var task = new MetronomeGoalTask();
            task.Id = 23;
            task.Title = "Hello World";

            var anotherTask = new MetronomeGoalTask();
            anotherTask.Id = 23;
            anotherTask.Title = "Goodbye World";

            Assert.That(task, Is.EqualTo(anotherTask));
        }

        [Test]
        public void New_MetronomeGoalTask_StartDate_IsNull()
        {
            var task = new MetronomeGoalTask();
            task.Title = "Task 1";
            task.StartSpeed = 80;
            task.TargetSpeed = 120;

            Assert.That(task.StartDate, Is.Null);
        }

        [Test]
        public void New_MetronomeGoalTask_CreateDate_IsSet()
        {
            var task = new MetronomeGoalTask();
            task.Title = "Task 1";
            task.StartSpeed = 80;
            task.TargetSpeed = 120;
            Assert.That(task.CreateDate, Is.Not.EqualTo(DateTime.MinValue));
        }

        [Test]
        public void New_MetronomeGoalTask_Has_0_MinutesPracticed()
        {
            var task = new MetronomeGoalTask();
            task.Title = "Task 1";
            task.StartSpeed = 80;
            task.TargetSpeed = 120;
            Assert.That(task.MinutesPracticed, Is.EqualTo(0));
        }

        [Test]
        public void New_MetronomeGoalTask_Has_0_PercentCompleted()
        {
            var task = new MetronomeGoalTask();
            task.Title = "Task 1";
            task.StartSpeed = 80;
            task.TargetSpeed = 120;
            Assert.That(task.PercentCompleted, Is.EqualTo(0));
        }

        [Test]
        public void New_MetronomeGoalTask_Has_0_EffortWeighting()
        {
            var task = new MetronomeGoalTask();
            task.Title = "Task 1";
            task.StartSpeed = 80;
            task.TargetSpeed = 120;
            Assert.That(task.Weighting, Is.EqualTo(0));
        }

        [Test]
        public void Existing_MetronomeGoalTask_Only_Half_Way_To_Target_Returns_50_Percent_Completed()
        {
            // The idea is that if one starts off with a metronome speed of 50, but your 
            // target metronome speed is 90, and your currently checked off speed is 70, then your percentage will be...
            // ((90 - 70) / (90 - 50)) * 100 ...

            List<MetronomeSessionResult> results = new List<MetronomeSessionResult>()
            {
                new MetronomeSessionResult(DateTime.Parse("2018/06/22 18:00:00"), DateTime.Parse("2018/06/22 18:05:00"), 60),
                new MetronomeSessionResult(DateTime.Parse("2018/06/22 18:10:00"), DateTime.Parse("2018/06/22 18:25:00"), 80)
            };

            var task = new MetronomeGoalTask();
            task.Title = "Task 1";
            task.StartSpeed = 60;
            task.TargetSpeed = 100;
            task.CreateDate = DateTime.Parse("2018/06/22 18:33:20");
            task.AddSessionRange(results);
            Assert.That(task.PercentCompleted, Is.EqualTo(50));
        }

        [Test]
        public void Existing_MetronomeGoalTask_With_Empty_ResultList_Is_Zero_Percent_Completed()
        {
            List<MetronomeSessionResult> results = new List<MetronomeSessionResult>() { };

            MetronomeGoalTask task = new MetronomeGoalTask();
            task.Title = "Task 1";
            task.StartSpeed = 60;
            task.TargetSpeed = 100;
            task.CreateDate = DateTime.Parse("2018/06/22 18:33:20");
            task.AddSessionRange(results);

            Assert.That(task.PercentCompleted, Is.EqualTo(0));
        }

        [Test]
        public void Existing_MetronomeGoalTask_With_Start_Speed_Bigger_Than_Current_Speed_Is_Zero_Percent_Completed()
        {
            List<MetronomeSessionResult> results = new List<MetronomeSessionResult>()
            {
                new MetronomeSessionResult(DateTime.Parse("2018/06/22 18:00:00"), DateTime.Parse("2018/06/22 18:05:00"), 40),
                new MetronomeSessionResult(DateTime.Parse("2018/06/22 18:10:00"), DateTime.Parse("2018/06/22 18:25:00"), 45)
            };

            MetronomeGoalTask task = new MetronomeGoalTask();
            task.Title = "Task 1";
            task.StartSpeed = 60;
            task.TargetSpeed = 100;
            task.CreateDate = DateTime.Parse("2018/06/22 18:33:20");
            task.AddSessionRange(results);

            Assert.That(task.PercentCompleted, Is.EqualTo(0));
        }

        [Test]
        public void Existing_MetronomeGoalTask_With_Adds_Up_SessionResult_Minutes_Correctly()
        {
            List<MetronomeSessionResult> results = new List<MetronomeSessionResult>()
            {
                new MetronomeSessionResult(DateTime.Parse("2018/06/22 18:00:00"), DateTime.Parse("2018/06/22 18:05:00"), 40),
                new MetronomeSessionResult(DateTime.Parse("2018/06/22 18:10:00"), DateTime.Parse("2018/06/22 18:25:00"), 45)
            };

            MetronomeGoalTask task = new MetronomeGoalTask();
            task.Title = "Task 1";
            task.StartSpeed = 60;
            task.TargetSpeed = 100;
            task.CreateDate = DateTime.Parse("2018/06/22 18:33:20");
            task.AddSessionRange(results);

            Assert.That(task.MinutesPracticed, Is.EqualTo(20));
        }
        [Test]
        public void MetronomeSessionResult_Given_Value_Below_0_Throws_Exception()
        {
            TestDelegate proc = () => new MetronomeSessionResult(DateTime.Parse("2018/06/22 18:00:00"), DateTime.Parse("2018/06/22 18:05:00"), -1);
            Assert.That(proc, Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Existing_MetronomeGoalTask_Supplies_Correct_StartDate_From_SessionList()
        {
            DateTime expectedStartTime = DateTime.Parse("2018/06/22 18:00:00");

            List<MetronomeSessionResult> results = new List<MetronomeSessionResult>()
            {
                new MetronomeSessionResult(DateTime.Parse("2018/06/22 18:00:00"), DateTime.Parse("2018/06/22 18:05:00"), 60),
                new MetronomeSessionResult(DateTime.Parse("2018/06/22 18:10:00"), DateTime.Parse("2018/06/22 18:25:00"), 60)
            };

            MetronomeGoalTask task = new MetronomeGoalTask();

            task.Title = "Task 1";
            task.StartSpeed = 60;
            task.TargetSpeed = 70;
            task.CreateDate = DateTime.Parse("2018/03/18 18:01:20");
            task.AddSessionRange(results);

            Assert.That(task.StartDate, Is.EqualTo(expectedStartTime));
        }
    }
}
