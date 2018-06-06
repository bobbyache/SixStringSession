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
        //[Test]
        //public void Weighting_Single_PercentGoalTask_Added_To_Task_Creates_100_Percent_Weighting()
        //{
        //    Goal goal = new Goal();
        //    IGoalTask goalTask = goal.AddTask("Title 1", GoalTaskType.Percent);
        //    Assert.That(goalTask.Weighting, Is.EqualTo(100));
        //}

        [Test]
        public void Weighting_Single_PercentGoalTask_Added_To_Task_Creates_100_Percent_Weighting()
        {
            Goal goal = new Goal(1000);
            GoalTask goalTask = new PercentGoalTask("Title 1");
            goalTask.Weighting = 1000;
            goal.AddTask(goalTask);
            Assert.That(goalTask.Weighting, Is.EqualTo(1000));
        }


        [Test]
        public void Weighting_Single_MetronomeGoalTask_Added_To_Task_Creates_100_Percent_Weighting()
        {
            Goal goal = new Goal(1000);
            GoalTask goalTask = new MetronomeGoalTask("Title 1", 80, 90);
            goalTask.Weighting = 1000;
            goal.AddTask(goalTask);
            Assert.That(goalTask.Weighting, Is.EqualTo(1000));
        }


        [Test]
        public void Weighting_Single_DurationGoalTask_Added_To_Task_Creates_100_Percent_Weighting()
        {
            Goal goal = new Goal(1000);
            GoalTask goalTask = new DurationGoalTask("Title 1", 100);
            goalTask.Weighting = 1000;
            goal.AddTask(goalTask);
            Assert.That(goalTask.Weighting, Is.EqualTo(1000));
        }

        [Test]
        public void Goal_Add_DurationTask_Adds_Task_Successfully()
        {
            Goal goal = new Goal(1000);
            GoalTask goalTask = new DurationGoalTask("Title 1", 100);

            Assert.That(goalTask, Is.TypeOf<DurationGoalTask>());
            Assert.IsFalse(string.IsNullOrEmpty(goalTask.Id));
            Assert.That(goalTask.Title, Is.EqualTo("Title 1"));
        }

        [Test]
        public void Goal_Add_PercentTask_Adds_Task_Successfully()
        {
            Goal goal = new Goal(1000);
            GoalTask goalTask = new PercentGoalTask("Title 1");

            Assert.That(goalTask, Is.TypeOf<PercentGoalTask>());
            Assert.IsFalse(string.IsNullOrEmpty(goalTask.Id));
            Assert.That(goalTask.Title, Is.EqualTo("Title 1"));
        }

        [Test]
        public void Goal_Add_MetronomeTask_Adds_Task_Successfully()
        {
            Goal goal = new Goal(1000);
            GoalTask goalTask = new MetronomeGoalTask("Title 1", 80, 90);

            Assert.That(goalTask, Is.TypeOf<MetronomeGoalTask>());
            Assert.IsFalse(string.IsNullOrEmpty(goalTask.Id));
            Assert.That(goalTask.Title, Is.EqualTo("Title 1"));
        }

        [Test]
        public void Goal_New_With_Null_Tasks_Has_Zero_Tasks()
        {
            IGoalFile[] goalFiles = new IGoalFile[]
            {
                new Mock<IGoalFile>().Object,
                new Mock<IGoalFile>().Object
            };

            Goal goal = new Goal(1000, null, goalFiles);
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

            Goal goal = new Goal(1000, goalTasks, null);
            Assert.That(goal.FileCount, Is.EqualTo(0));
            Assert.That(goal.Files.Length == 0);
        }

        [Test]
        public void Goal_New_Has_No_Minutes_Recorded()
        {
            Goal goal = new Goal(1000);
            Assert.AreEqual(0, goal.MinutesPracticed);
        }

        [Test]
        public void Goal_New_Has_Zero_TaskCount()
        {
            Goal goal = new Goal(1000);
            Assert.AreEqual(0, goal.TaskCount);
        }

        [Test]
        public void Goal_New_Has_Zero_Weighting()
        {
            Goal goal = new Goal(1000);
            Assert.AreEqual(0, goal.Weighting);
        }

        [Test]
        public void Goal_New_Has_Set_CreateDate()
        {
            Goal goal = new Goal(1000);
            Assert.AreNotEqual(DateTime.MinValue, goal.CreateDate);
            Assert.AreNotEqual(DateTime.MaxValue, goal.CreateDate);
        }

        [Test]
        public void Goal_New_Is_Not_Considered_Complete()
        {
            Goal goal = new Goal(1000);
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
            
            Goal goal = new Goal(1000, goalTasks, goalFiles);
            Assert.AreEqual(2, goal.FileCount);
            Assert.AreEqual(2, goal.Files.Length);
            Assert.AreEqual(2, goal.TaskCount);
            Assert.AreEqual(2, goal.Tasks.Length);
        }

        [Test]
        public void Goal_With_Two_Unequally_Weighted_Tasks_Returns_Correct_Percent_Complete()
        {
            Goal goal = new Goal(100);

            MetronomeGoalTask task1 = new MetronomeGoalTask("Metronome Task", 100, 120);
            task1.Weighting = 50;

            DurationGoalTask task2 = new DurationGoalTask("Duration Task", 60);
            task2.Weighting = 100;

            PercentGoalTask task3 = new PercentGoalTask("Percent Task");
            task3.Weighting = 50;

            goal.AddTask(task1);
            goal.AddTask(task2);
            goal.AddTask(task3);

            // all exactly half done...
            task1.AddSession(new MetronomeSessionResult(DateTime.Parse("2018/06/22 18:33:20"), DateTime.Parse("2018/06/22 18:38:20"), 110));
            task2.AddSession(new DurationSessionResult(DateTime.Parse("2018/06/22 18:00:00"), DateTime.Parse("2018/06/22 18:30:00")));
            task3.AddSession(new PercentSessionResult(DateTime.Parse("2018/06/22 18:00:00"), DateTime.Parse("2018/06/22 18:30:00"), 50));

            Assert.That(goal.PercentComplete, Is.EqualTo(50));
        }

        [Test]
        public void Goal_With_No_Tasks_Returns_0_Percent_Complete()
        {
            Goal goal = new Goal(100);
            Assert.That(goal.PercentComplete, Is.EqualTo(0));
        }

        [Test]
        public void Goal_AddTask_With_0_Weighting_Throws_Exception()
        {
            Goal goal = new Goal(100);

            MetronomeGoalTask task1 = new MetronomeGoalTask("Metronome Task", 100, 120);
            task1.Weighting = 0;

            TestDelegate proc = () => goal.AddTask(task1);
            Assert.That(proc, Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Goal_AddTask_With_Negative_Weighting_Throws_Exception()
        {
            Goal goal = new Goal(100);

            MetronomeGoalTask task1 = new MetronomeGoalTask("Metronome Task", 100, 120);
            task1.Weighting = -1;

            TestDelegate proc = () => goal.AddTask(task1);
            Assert.That(proc, Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Goal_AddTask_With_Weighting_That_Exceeds_Limit_Throws_Exception()
        {
            Goal goal = new Goal(100);

            MetronomeGoalTask task1 = new MetronomeGoalTask("Metronome Task", 100, 120);
            task1.Weighting = 101;

            TestDelegate proc = () => goal.AddTask(task1);
            Assert.That(proc, Throws.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}
