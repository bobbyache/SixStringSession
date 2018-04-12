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
            MetronomeGoalTask task = new MetronomeGoalTask();
            Assert.That(task.StartDate, Is.Null);
        }

        [Test]
        public void New_MetronomeGoalTask_CreateDate_IsSet()
        {
            MetronomeGoalTask task = new MetronomeGoalTask();
            Assert.That(task.CreateDate, Is.Not.EqualTo(DateTime.MinValue));
        }

        [Test]
        public void New_MetronomeGoalTask_Has_0_MinutesPracticed()
        {
            MetronomeGoalTask task = new MetronomeGoalTask();
            Assert.That(task.MinutesPracticed, Is.EqualTo(0));
        }

        [Test]
        public void New_MetronomeGoalTask_Has_0_PercentCompleted()
        {
            MetronomeGoalTask task = new MetronomeGoalTask();
            Assert.That(task.PercentCompleted, Is.EqualTo(0));
        }

        [Test]
        public void New_MetronomeGoalTask_Has_0_EffortWeighting()
        {
            MetronomeGoalTask task = new MetronomeGoalTask();
            Assert.That(task.Weighting, Is.EqualTo(0));
        }

    }
}
