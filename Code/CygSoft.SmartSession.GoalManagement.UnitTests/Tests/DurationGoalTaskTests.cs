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
            Assert.That(task.TargetUnit, Is.EqualTo(0));
        }

        [Test]
        public void New_DurationGoalTask_Has_Default_Hour_Unit()
        {
            DurationGoalTask task = new DurationGoalTask("Task 1");
            Assert.That(task.TimeUnit, Is.EqualTo("Hour"));
        }
    }
}
