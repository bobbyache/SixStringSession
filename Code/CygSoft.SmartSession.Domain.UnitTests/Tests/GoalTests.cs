
using CygSoft.SmartSession.Domain.Goals;
using CygSoft.SmartSession.DomainLegacy;
using CygSoft.SmartSession.Infrastructure;
using Moq;
using NUnit.Framework;
using System;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class GoalTests
    {
        [Test]
        public void Test()
        {
            var goal = new OldGoal();
            var task1 = new PercentGoalTask { Id = 1, CreateDate = DateTime.Now, Title = "Task 1", Weighting = 50 };
            var task2 = new PercentGoalTask { Id = 1, CreateDate = DateTime.Now, Title = "Task 2", Weighting = 50 };

            goal.AddTask(task1);
            goal.AddTask(task2);

            task1.AddSession(new PercentSessionResult(DateTime.Now, DateTime.Now.AddMinutes(10), 50));
            task2.AddSession(new PercentSessionResult(DateTime.Now, DateTime.Now.AddMinutes(10), 50));

            Assert.That(goal.PercentComplete, Is.EqualTo(50));
            Assert.That(goal.IsConsideredComplete, Is.False);
        }

        [Test]
        public void Goal_With_Four_PercentTasks_At_Varying_Percents_And_Weightings_Returns_Correct_PercentComplete()
        {
            var goal = new OldGoal();

            var task1 = new PercentGoalTask { Id = 1, CreateDate = DateTime.Now, Title = "Task 1", Weighting = 25  };
            var task2 = new PercentGoalTask { Id = 1, CreateDate = DateTime.Now, Title = "Task 2", Weighting = 40 };
            var task3 = new PercentGoalTask { Id = 1, CreateDate = DateTime.Now, Title = "Task 3", Weighting = 60 };
            var task4 = new PercentGoalTask { Id = 1, CreateDate = DateTime.Now, Title = "Task 4", Weighting = 75 };

            goal.AddTask(task1);
            goal.AddTask(task2);
            goal.AddTask(task3);
            goal.AddTask(task4);

            task1.AddSession(new PercentSessionResult(DateTime.Now, DateTime.Now.AddMinutes(10), 100));
            task2.AddSession(new PercentSessionResult(DateTime.Now, DateTime.Now.AddMinutes(10), 60));
            task3.AddSession(new PercentSessionResult(DateTime.Now, DateTime.Now.AddMinutes(10), 40));
            task4.AddSession(new PercentSessionResult(DateTime.Now, DateTime.Now.AddMinutes(10), 25));

            Assert.That(goal.PercentComplete, Is.InRange(45.87, 45.9));
            Assert.That(goal.IsConsideredComplete, Is.False);
        }

        [Test]
        public void One_Goal_Equals_Another_Goal_Correctly()
        {
            var goal = new OldGoal();
            goal.Id = 23;
            goal.Title = "Hello World";

            var anotherGoal = new OldGoal();
            anotherGoal.Id = 23;
            anotherGoal.Title = "Goodbye World";

            Assert.That(goal, Is.EqualTo(anotherGoal));
        }


        [Test]
        public void Weighting_Single_PercentGoalTask_Added_To_Task_Creates_100_Percent_Weighting()
        {
            var goal = new OldGoal();
            var task = new PercentGoalTask
            {
                Title = "Task 1",
                Weighting = 100
            };

            goal.AddTask(task);
            Assert.That(task.Weighting, Is.EqualTo(100));
        }


        [Test]
        public void Weighting_Single_MetronomeGoalTask_Added_To_Task_Creates_100_Percent_Weighting()
        {
            var goal = new OldGoal();
            var task = new PercentGoalTask
            {
                Title = "Task 1",
                Weighting = 100
            };

            goal.AddTask(task);
            Assert.That(task.Weighting, Is.EqualTo(100));
        }


        [Test]
        public void Weighting_Single_DurationGoalTask_Added_To_Task_Creates_100_Percent_Weighting()
        {
            var goal = new OldGoal();
            var goalTask = new DurationGoalTask
            {
                TargetMinutes = 100,
                Title = "Title 1",
                Weighting = 100
            };

            goal.AddTask(goalTask);
            Assert.That(goalTask.Weighting, Is.EqualTo(100));
        }

        [Test]
        public void Goal_Add_DurationTask_Adds_Task_Successfully()
        {
            var goal = new OldGoal();
            var goalTask = new DurationGoalTask
            {
                TargetMinutes = 100,
                Title = "Title 1"
            };

            Assert.That(goalTask, Is.TypeOf<DurationGoalTask>());
            Assert.IsTrue(goalTask.Id == -1);
            Assert.That(goalTask.Title, Is.EqualTo("Title 1"));
        }

        [Test]
        public void Goal_Add_PercentTask_Adds_Task_Successfully()
        {
            var goal = new OldGoal();
            var goalTask = new PercentGoalTask
            {
                Title = "Title 1"
            };

            Assert.That(goalTask, Is.TypeOf<PercentGoalTask>());
            Assert.IsTrue(goalTask.Id == -1);
            Assert.That(goalTask.Title, Is.EqualTo("Title 1"));
        }

        [Test]
        public void Goal_Add_MetronomeTask_Adds_Task_Successfully()
        {
            var goal = new OldGoal();
            var goalTask = new MetronomeGoalTask
            {
                StartSpeed = 80,
                TargetSpeed = 90,
                Title = "Title 1"
            };

            Assert.That(goalTask, Is.TypeOf<MetronomeGoalTask>());
            Assert.IsTrue(goalTask.Id == -1);
            Assert.That(goalTask.Title, Is.EqualTo("Title 1"));
        }

        [Test]
        public void Goal_New_With_Null_Tasks_Has_Zero_Tasks()
        {
            var goal = new OldGoal(null);
            Assert.That(goal.TaskCount, Is.EqualTo(0));
            Assert.That(goal.Tasks.Length == 0);
        }

        [Test]
        public void Goal_New_Has_No_Minutes_Recorded()
        {
            var goal = new OldGoal();
            Assert.AreEqual(0, goal.MinutesPracticed);
        }

        [Test]
        public void Goal_New_Has_Zero_TaskCount()
        {
            var goal = new OldGoal();
            Assert.AreEqual(0, goal.TaskCount);
        }

        [Test]
        public void Goal_New_Has_Zero_Weighting()
        {
            var goal = new OldGoal();
            Assert.AreEqual(0, goal.Weighting);
        }

        [Test]
        public void Goal_New_Has_Set_CreateDate()
        {
            var goal = new OldGoal();
            Assert.AreNotEqual(DateTime.MinValue, goal.CreateDate);
            Assert.AreNotEqual(DateTime.MaxValue, goal.CreateDate);
        }

        [Test]
        public void Goal_New_Is_Not_Considered_Complete()
        {
            var goal = new OldGoal();
            Assert.AreEqual(false, goal.IsConsideredComplete);
        }

        [Test]
        public void Goal_Existing_Has_SetState_Via_Constructor()
        {
            IEditableGoalTask[] goalTasks = new IEditableGoalTask[]
            {
                new Mock<IEditableGoalTask>().Object,
                new Mock<IEditableGoalTask>().Object
            };

            var goal = new OldGoal(goalTasks);
            Assert.AreEqual(2, goal.TaskCount);
            Assert.AreEqual(2, goal.Tasks.Length);
        }

        [Test]
        public void Goal_With_Two_Unequally_Weighted_Tasks_Returns_Correct_Percent_Complete()
        {
            var goal = new OldGoal();

            var task1 = new MetronomeGoalTask
            {
                StartSpeed = 100,
                TargetSpeed = 120,
                Title = "Metronome Task",
                Weighting = 50
            };

            var task2 = new DurationGoalTask
            {
                Weighting = 100,
                TargetMinutes = 60,
                Title = "Duration Task"
            };

            var task3 = new PercentGoalTask();
            task1.Title = "Percent Task";
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
            var goal = new OldGoal();
            Assert.That(goal.PercentComplete, Is.EqualTo(0));
        }

        [Test]
        public void Goal_AddTask_With_0_Weighting_Throws_Exception()
        {
            var goal = new OldGoal();

            var task1 = new MetronomeGoalTask();
            task1.Title = "Metronome Task";
            task1.StartSpeed = 100;
            task1.TargetSpeed = 120;
            task1.Weighting = 0;

            TestDelegate proc = () => goal.AddTask(task1);
            Assert.That(proc, Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Goal_AddTask_With_Negative_Weighting_Throws_Exception()
        {
            var goal = new OldGoal();

            var task1 = new MetronomeGoalTask();
            task1.Title = "Metronome Task";
            task1.StartSpeed = 100;
            task1.TargetSpeed = 120;
            task1.Weighting = -1;

            TestDelegate proc = () => goal.AddTask(task1);
            Assert.That(proc, Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Goal_AddTask_With_Weighting_That_Exceeds_Limit_Throws_Exception()
        {
            var goal = new OldGoal();

            var task1 = new MetronomeGoalTask();
            task1.Title = "Metronome Task";
            task1.StartSpeed = 100;
            task1.TargetSpeed = 120;
            task1.Weighting = 101;

            TestDelegate proc = () => goal.AddTask(task1);
            Assert.That(proc, Throws.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}
