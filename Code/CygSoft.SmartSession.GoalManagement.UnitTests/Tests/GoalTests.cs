using CygSoft.SmartSession.GoalManagement.Infrastructure;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement.UnitTests.Tests
{
    [TestFixture]
    public class GoalTests
    {
        [Test]
        public void Goal_New_Has_No_Minutes_Recorded()
        {
            Goal goal = new Goal();
            Assert.AreEqual(0, goal.MinutesPracticed);
        }

        [Test]
        public void Goal_New_Has_Zero_TaskCount()
        {
            Goal goal = new Goal();
            Assert.AreEqual(0, goal.TaskCount);
        }

        [Test]
        public void Goal_New_Has_Zero_Weighting()
        {
            Goal goal = new Goal();
            Assert.AreEqual(0, goal.Weighting);
        }

        [Test]
        public void Goal_New_Has_Set_CreateDate()
        {
            Goal goal = new Goal();
            Assert.AreNotEqual(DateTime.MinValue, goal.CreateDate);
            Assert.AreNotEqual(DateTime.MaxValue, goal.CreateDate);
        }

        [Test]
        public void Goal_New_Is_Not_Considered_Complete()
        {
            Goal goal = new Goal();
            Assert.AreEqual(false, goal.IsConsideredComplete);
        }

        [Test]
        public void Goal_Existing_Has_SetState_Via_Constructor()
        {
            int minutesRecorded = 12;
            IGoalTask[] goalTasks = new IGoalTask[]
            {
                new Mock<IGoalTask>().Object,
                new Mock<IGoalTask>().Object
            };

            IGoalFile[] goalFiles = new IGoalFile[]
            {
                new Mock<IGoalFile>().Object,
                new Mock<IGoalFile>().Object
            };
            
            Goal goal = new Goal(minutesRecorded, goalTasks, goalFiles);
            Assert.AreEqual(12, goal.MinutesPracticed);
            Assert.AreEqual(2, goal.FileCount);
            Assert.AreEqual(2, goal.Files.Length);
            Assert.AreEqual(2, goal.TaskCount);
            Assert.AreEqual(2, goal.Tasks.Length);
        }
    }
}
