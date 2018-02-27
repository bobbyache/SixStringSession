using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement.UnitTests
{
    [TestFixture]
    public class GoalTests
    {
        [Test]
        public void Goal_New_Has_No_Minutes_Recorded()
        {
            Goal goal = new Goal();
            Assert.AreEqual(0, goal.MinutesRecorded);
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
            Assert.AreEqual(12, goal.MinutesRecorded);
            Assert.AreEqual(2, goal.FileCount);
            Assert.AreEqual(2, goal.Files.Length);
            Assert.AreEqual(2, goal.TaskCount);
            Assert.AreEqual(2, goal.Tasks.Length);
        }
    }
}
