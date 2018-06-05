using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement.UnitTests.Tests
{
    [TestFixture]
    public class MetronomeGoalTaskTests
    {
        [Test]
        public void New_MetronomeGoalTask_StartDate_IsNull()
        {
            MetronomeGoalTask task = new MetronomeGoalTask("Task 1", 80, 120);
            Assert.That(task.StartDate, Is.Null);
        }

        [Test]
        public void New_MetronomeGoalTask_CreateDate_IsSet()
        {
            MetronomeGoalTask task = new MetronomeGoalTask("Task 1", 80, 120);
            Assert.That(task.CreateDate, Is.Not.EqualTo(DateTime.MinValue));
        }

        [Test]
        public void New_MetronomeGoalTask_Has_0_MinutesPracticed()
        {
            MetronomeGoalTask task = new MetronomeGoalTask("Task 1", 80, 120);
            Assert.That(task.MinutesPracticed, Is.EqualTo(0));
        }

        [Test]
        public void New_MetronomeGoalTask_Has_0_PercentCompleted()
        {
            MetronomeGoalTask task = new MetronomeGoalTask("Task 1", 80, 120);
            Assert.That(task.PercentCompleted, Is.EqualTo(0));
        }

        [Test]
        public void New_MetronomeGoalTask_Has_0_EffortWeighting()
        {
            MetronomeGoalTask task = new MetronomeGoalTask("Task 1", 80, 120);
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

            MetronomeGoalTask task = new MetronomeGoalTask("Task 1", DateTime.Parse("2018/06/22 18:33:20"), 60, 100, results);
            Assert.That(task.PercentCompleted, Is.EqualTo(50));
        }

        [Test]
        public void Existing_MetronomeGoalTask_With_Empty_ResultList_Is_Zero_Percent_Completed()
        {
            List<MetronomeSessionResult> results = new List<MetronomeSessionResult>() { };

            MetronomeGoalTask task = new MetronomeGoalTask("Task 1", DateTime.Parse("2018/06/22 18:33:20"), 60, 100, results);
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

            MetronomeGoalTask task = new MetronomeGoalTask("Task 1", DateTime.Parse("2018/06/22 18:33:20"), 60, 100, results);
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

            MetronomeGoalTask task = new MetronomeGoalTask("Task 1", DateTime.Parse("2018/06/22 18:33:20"), 60, 100, results);
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


            MetronomeGoalTask task = new MetronomeGoalTask("Task 1", DateTime.Parse("2018/03/18 18:01:20"), 60, 70, results);
            Assert.That(task.StartDate, Is.EqualTo(expectedStartTime));
        }

        [Test]
        public void MetronomeSessionResult_Given_StartValue_Higher_Than_Target_Throws_Exception()
        {
            TestDelegate proc = () => new MetronomeGoalTask("Title 1", DateTime.Parse("2018/06/22 18:33:20"), 61, 60, 
                new List<MetronomeSessionResult>());
            Assert.That(proc, Throws.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}
