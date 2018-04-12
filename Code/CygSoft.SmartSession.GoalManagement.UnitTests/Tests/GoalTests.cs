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
        public void Weighting_Single_PercentGoalTask_Added_To_Task_Creates_100_Percent_Weighting()
        {
            Goal goal = new Goal();
            IGoalTask goalTask = goal.AddTask("Title 1", GoalTaskType.Percent);
            Assert.That(goalTask.Weighting, Is.EqualTo(100));
        }


        [Test]
        public void Weighting_Single_MetronomeGoalTask_Added_To_Task_Creates_100_Percent_Weighting()
        {
            Goal goal = new Goal();
            IGoalTask goalTask = goal.AddTask("Title 1", GoalTaskType.Metronome);
            Assert.That(goalTask.Weighting, Is.EqualTo(100));
        }


        [Test]
        public void Weighting_Single_DurationGoalTask_Added_To_Task_Creates_100_Percent_Weighting()
        {
            Goal goal = new Goal();
            IGoalTask goalTask = goal.AddTask("Title 1", GoalTaskType.Duration);
            Assert.That(goalTask.Weighting, Is.EqualTo(100));
        }

        [Test]
        public void Goal_Add_DurationTask_Adds_Task_Successfully()
        {
            Goal goal = new Goal();
            IGoalTask task = goal.AddTask("Task 1", GoalTaskType.Duration);

            Assert.That(task, Is.TypeOf<DurationGoalTask>());
            Assert.IsFalse(string.IsNullOrEmpty(task.Id));
            Assert.That(task.Title, Is.EqualTo("Task 1"));
        }

        [Test]
        public void Goal_Add_PercentTask_Adds_Task_Successfully()
        {
            Goal goal = new Goal();
            IGoalTask task = goal.AddTask("Task 1", GoalTaskType.Percent);

            Assert.That(task, Is.TypeOf<PercentGoalTask>());
            Assert.IsFalse(string.IsNullOrEmpty(task.Id));
            Assert.That(task.Title, Is.EqualTo("Task 1"));
        }

        [Test]
        public void Goal_Add_MetronomeTask_Adds_Task_Successfully()
        {
            Goal goal = new Goal();
            IGoalTask task = goal.AddTask("Task 1", GoalTaskType.Metronome);

            Assert.That(task, Is.TypeOf<MetronomeGoalTask>());
            Assert.IsFalse(string.IsNullOrEmpty(task.Id));
            Assert.That(task.Title, Is.EqualTo("Task 1"));
        }

        [Test]
        public void Goal_New_With_Null_Tasks_Has_Zero_Tasks()
        {
            IGoalFile[] goalFiles = new IGoalFile[]
            {
                new Mock<IGoalFile>().Object,
                new Mock<IGoalFile>().Object
            };

            Goal goal = new Goal(null, goalFiles);
            Assert.That(goal.TaskCount, Is.EqualTo(0));
            Assert.That(goal.Tasks.Length == 0);
        }

        [Test]
        public void Goal_New_With_Null_Files_Has_Zero_Files()
        {
            IGoalTask[] goalTasks = new IGoalTask[]
            {
                new Mock<IGoalTask>().Object,
                new Mock<IGoalTask>().Object
            };

            Goal goal = new Goal(goalTasks, null);
            Assert.That(goal.FileCount, Is.EqualTo(0));
            Assert.That(goal.Files.Length == 0);
        }

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
            
            Goal goal = new Goal(goalTasks, goalFiles);
            Assert.AreEqual(2, goal.FileCount);
            Assert.AreEqual(2, goal.Files.Length);
            Assert.AreEqual(2, goal.TaskCount);
            Assert.AreEqual(2, goal.Tasks.Length);
        }
    }
}
