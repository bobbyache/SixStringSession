using Moq;
using SmartClient.Domain.Common;
using SmartClient.Domain.Data;
using SmartClient.Domain.Exceptions;
using SmartClient.Domain.Tests.Test;
using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Xunit;

namespace SmartClient.Domain.Tests
{
    // InternalsVisibleTo in a .net CORE project
    // https://www.meziantou.net/declaring-internalsvisibleto-in-the-csproj.htm
    public class GoalTests
    {
        private const string PATH_TO_FILE = @"\\path\to\file";

        [Fact]
        public void GetATaskSummaryGivenATaskId()
        {
            var goalManager = MockHelpers.GetMockGoalManager();
            goalManager.Open(PATH_TO_FILE);

            var taskSummary = goalManager.GetTaskSummary(MockHelpers.TASK_1_ID);

            Assert.True(taskSummary != null);
            Assert.True(taskSummary.Id == MockHelpers.TASK_1_ID);
            Assert.Equal(MockHelpers.TASK_1_TITLE, taskSummary.Title);
            Assert.True(taskSummary.PercentProgress == 75);
            Assert.Equal(TaskUnitOfMeasure.BPM, taskSummary.UnitOfMeasure);
            Assert.True(taskSummary.Weighting == 0.5);
            Assert.True(taskSummary.GoalTitle == MockHelpers.GOAL_TITLE);
        }

        [Fact]
        public void GetAllTaskSummaries()
        {
            var goalManager = MockHelpers.GetMockGoalManager();
            goalManager.Open(PATH_TO_FILE);

            var taskSummaries = goalManager.GetTaskSummaries();

            Assert.True(taskSummaries[0] != null);
            Assert.True(taskSummaries[0].Id == MockHelpers.TASK_1_ID);
            Assert.True(taskSummaries[0].Title == MockHelpers.TASK_1_TITLE);
            Assert.True(taskSummaries[0].PercentProgress == 75);
            Assert.True(taskSummaries[0].UnitOfMeasure == TaskUnitOfMeasure.BPM);
            Assert.True(taskSummaries[0].Weighting == 0.5);
            Assert.True(taskSummaries[0].GoalTitle == MockHelpers.GOAL_TITLE);

            Assert.True(taskSummaries[1] != null);
            Assert.True(taskSummaries[1].Id == MockHelpers.TASK_2_ID);
            Assert.True(taskSummaries[1].Title == MockHelpers.TASK_2_TITLE);
            Assert.True(taskSummaries[1].PercentProgress == 90);
            Assert.True(taskSummaries[1].Weighting == 0.5);
            Assert.True(taskSummaries[1].GoalTitle == MockHelpers.GOAL_TITLE);
        }

        [Fact]
        public void GetTaskProgressSnapshotsForTask()
        {
            var goalManager = MockHelpers.GetMockGoalManager();
            goalManager.Open(PATH_TO_FILE);

            var goalSummary = goalManager.GetSummary();
            var snapshots = goalManager.GetTaskProgressSnapshots(MockHelpers.TASK_2_ID);

            Assert.Equal(3, snapshots.Count());
            Assert.Equal(new DateTime(2010, 3, 15), snapshots[0].Day);
            Assert.Equal(10, snapshots[0].Value);
            Assert.Equal(new DateTime(2010, 5, 17), snapshots[2].Day);
        }

        [Fact]
        public void GetGoalSummary()
        {
            var goalManager = MockHelpers.GetMockGoalManager();
            goalManager.Open(PATH_TO_FILE);

            var goalSummary = goalManager.GetSummary();

            Assert.Equal(MockHelpers.GOAL_ID, goalSummary.Id);
            Assert.Equal(MockHelpers.GOAL_TITLE, goalSummary.Title);
            Assert.Equal(82, goalSummary.PercentProgress);
        }

        [Fact]
        public void GetGoalDetail()
        {
            var goalManager = MockHelpers.GetMockGoalManager();
            goalManager.Open(PATH_TO_FILE);

            var goalDetail = goalManager.GetDetail();

            Assert.Equal(MockHelpers.GOAL_ID, goalDetail.Id);
            Assert.Equal(MockHelpers.GOAL_TITLE, goalDetail.Title);
            Assert.Equal(82, goalDetail.PercentProgress);
            Assert.Equal(2, goalDetail.TaskSummaries.Length);
        }

        [Fact]
        public void UpdateGoal_EnsureTitleLengthValid()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            Assert.Throws<InvalidTitleException>(new Action(() => goalManager.GetEditableGoal().Title = "sh"));
        }

        [Fact]
        public void UpdateGoal_EnsureWeightingNotLessThanZero()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            Assert.Throws<InvalidWeightingException>(new Action(() => goalManager.GetEditableGoal().Weighting = -0.001));
        }

        [Fact]
        public void UpdateGoal_EnsureWeightingNotMoreThanOne()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            Assert.Throws<InvalidWeightingException>(new Action(() => goalManager.GetEditableGoal().Weighting = 1.001));
        }

        [Fact]
        public void GetTaskProgressSnapshot()
        {
            var goalManager = MockHelpers.GetMockGoalManager();
            goalManager.Open(PATH_TO_FILE);

            var progressSnapshot = goalManager.GetTaskProgressSnapshot(MockHelpers.TASK_2_ID, DateTime.Parse("2010-04-15 10:22:22"));
            Assert.NotNull(progressSnapshot);
        }


        [Fact]
        public void UpdateTaskProgressSnapshot_ExistingItem()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            goalManager.UpdateTaskProgressSnapshot(MockHelpers.TASK_2_ID, DateTime.Parse("2012-09-05 11:51:38"), 6);
            var snapshot = goalManager.GetTaskProgressSnapshot(MockHelpers.TASK_2_ID, DateTime.Parse("2012-09-05 10:22:22"));

            Assert.Equal(6, snapshot.Value);
        }

        [Fact]
        public void UpdateTaskProgressSnapshot_NewItem()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            goalManager.UpdateTaskProgressSnapshot(MockHelpers.TASK_2_ID, DateTime.Parse("2012-09-05 11:51:38"), 6);
            goalManager.UpdateTaskProgressSnapshot(MockHelpers.TASK_2_ID, DateTime.Parse("2014-10-15"), 7);
            var snapshot = goalManager.GetTaskProgressSnapshot(MockHelpers.TASK_2_ID, DateTime.Parse("2014-10-15 10:22:22"));

            Assert.Equal(7, snapshot.Value);
        }

        [Fact]
        public void UpdateTask_EnsureTitleLengthValid()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            var task = goalManager.GetEditableTask(MockHelpers.TASK_2_ID);

            Assert.Throws<InvalidTitleException>(new Action(() => task.Title = "sh"));
        }

        [Fact]
        public void UpdateTask_EnsureWeightingNotLessThanZero()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            var task = goalManager.GetEditableTask(MockHelpers.TASK_2_ID);

            Assert.Throws<InvalidWeightingException>(new Action(() => task.Weighting = -0.001));
        }

        [Fact]
        public void UpdateTask_EnsureWeightingNotMoreThanOne()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            var task = goalManager.GetEditableTask(MockHelpers.TASK_2_ID);

            Assert.Throws<InvalidWeightingException>(new Action(() => task.Weighting = 1.001));
        }

        [Fact]
        public void CreateATask()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            var editableTask = goalManager.CreateTask();

            Assert.NotNull(editableTask);

            Guid guidResult;
            Assert.True(Guid.TryParse(editableTask.Id, out guidResult), "Expected that ID would be a type of GUID");
            Assert.Equal("New Task", editableTask.Title);
            Assert.Equal(TaskUnitOfMeasure.BPM, editableTask.UnitOfMeasure);
            Assert.Equal(0, editableTask.Start);
            Assert.Equal(100, editableTask.Target);
            Assert.Equal(0.5, editableTask.Weighting);
        }

        [Fact]
        public void CreateATask_CreatesATaskOnGoal()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            var editableTask = goalManager.CreateTask();
            var taskSummary = goalManager.GetTaskSummary(editableTask.Id);

            Assert.NotNull(taskSummary);

            Guid guidResult;
            Assert.True(Guid.TryParse(editableTask.Id, out guidResult), "Expected that ID would be a type of GUID");

            Assert.Equal(0, taskSummary.PercentProgress);
            Assert.Equal(0.5, taskSummary.Weighting);
            Assert.Equal("New Task", taskSummary.Title);
            Assert.Equal(TaskUnitOfMeasure.BPM, editableTask.UnitOfMeasure);
            Assert.Equal(goalManager.GetSummary().Title, taskSummary.GoalTitle);
        }

        [Fact]
        public void GetEditableTask()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            var editableTask = goalManager.GetEditableTask(MockHelpers.GOAL_ID);

            Assert.NotNull(editableTask);

            Guid guidResult;
            Assert.True(Guid.TryParse(editableTask.Id, out guidResult), "Expected that ID would be a type of GUID");
            
            Assert.Equal(0.5, editableTask.Weighting);
            Assert.Equal(0, editableTask.Start);
            Assert.Equal(100, editableTask.Target);
            Assert.Equal(TaskUnitOfMeasure.BPM, editableTask.UnitOfMeasure);
            Assert.Equal(MockHelpers.TASK_2_TITLE, editableTask.Title);
            Assert.Equal(goalManager.GetSummary().Title, editableTask.GoalTitle);
        }

        [Fact]
        public void GetTaskDetail()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            GoalTaskDetail taskDetail = goalManager.GetTaskDetail("9a3c801b-5e5c-423c-9696-6a2f687f31db");

            Assert.Equal("9a3c801b-5e5c-423c-9696-6a2f687f31db", taskDetail.Id);
            Assert.Equal("Test Task 1", taskDetail.Title);
            Assert.Equal(0, taskDetail.Start);
            Assert.Equal(100, taskDetail.Target);
            Assert.Equal(0.5, taskDetail.Weighting);
            Assert.Equal(TaskUnitOfMeasure.BPM, taskDetail.UnitOfMeasure);

            Assert.Equal(3, taskDetail.TaskProgressSnapshots.Length);
        }

        [Fact]
        public void GetTaskDetail_EnsureProgressHistoryIsSorted()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            GoalTaskDetail taskDetail = goalManager.GetTaskDetail("9a3c801b-5e5c-423c-9696-6a2f687f31db");

        
            Assert.Equal(DateTime.Parse("2012-08-23"), taskDetail.TaskProgressSnapshots[0].Day);
            Assert.Equal(DateTime.Parse("2012-09-23"), taskDetail.TaskProgressSnapshots[2].Day);
        }

        [Fact]
        public void UpdateATask()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            var editableTask = goalManager.GetEditableTask(MockHelpers.GOAL_ID);

            editableTask.Title = "Changed Title";
            editableTask.Weighting = 0.75;
            editableTask.Start = 20;
            editableTask.Target = 120;
            editableTask.UnitOfMeasure = TaskUnitOfMeasure.MIN;

            // goalManager.UpdateTask(editableTask);

            var updatedTask = goalManager.GetEditableTask(MockHelpers.GOAL_ID);

            Assert.Equal("Changed Title", updatedTask.Title);
            Assert.Equal(0.75, updatedTask.Weighting);
            Assert.Equal(20, updatedTask.Start);
            Assert.Equal(120, updatedTask.Target);
            Assert.Equal(TaskUnitOfMeasure.MIN, editableTask.UnitOfMeasure);
        }

        [Fact]
        public void UpdateATask_EnsureTargetNotSetLessThanMaxProgressSnapshot()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            var editableTask = goalManager.CreateTask();
            var taskId = editableTask.Id;

            goalManager.UpdateTaskProgressSnapshot(editableTask.Id, DateTime.Parse("2012-09-05"), 10);
            goalManager.UpdateTaskProgressSnapshot(editableTask.Id, DateTime.Parse("2012-09-06"), 15);
            goalManager.UpdateTaskProgressSnapshot(editableTask.Id, DateTime.Parse("2012-09-07"), 20);

            Assert.Throws<OutOfProgressHistoryBoundsException>(new Action(() => editableTask.Target = 19));
        }

        [Fact]
        public void UpdateATask_EnsureTargetNotEqualTo_Start()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            var editableTask = goalManager.CreateTask();
            var taskId = editableTask.Id;

            Assert.Throws<OutOfProgressHistoryBoundsException>(new Action(() => editableTask.Target = 0));
        }

        [Fact]
        public void UpdateATask_EnsureTargetNotSmallerThan_Start()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            var editableTask = goalManager.CreateTask();
            var taskId = editableTask.Id;

            Assert.Throws<OutOfProgressHistoryBoundsException>(new Action(() => editableTask.Target = -1));
        }


        [Fact]
        public void UpdateATask_EnsureStartNotSetMoreThanMinProgressSnapshot()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            var editableTask = goalManager.CreateTask();
            var taskId = editableTask.Id;

            goalManager.UpdateTaskProgressSnapshot(editableTask.Id, DateTime.Parse("2012-09-05"), 10);
            goalManager.UpdateTaskProgressSnapshot(editableTask.Id, DateTime.Parse("2012-09-06"), 15);
            goalManager.UpdateTaskProgressSnapshot(editableTask.Id, DateTime.Parse("2012-09-07"), 20);

            Assert.Throws<OutOfProgressHistoryBoundsException>(new Action(() => editableTask.Start = 11));
        }

        [Fact]
        public void UpdateATask_EnsureStartNotEqualTo_Target()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            var editableTask = goalManager.CreateTask();
            var taskId = editableTask.Id;

            editableTask.Target = 100;

            Assert.Throws<OutOfProgressHistoryBoundsException>(new Action(() => editableTask.Start = 100));
        }

        [Fact]
        public void UpdateATask_EnsureStartNotBiggerThan_Target()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            var editableTask = goalManager.CreateTask();
            var taskId = editableTask.Id;

            editableTask.Target = 100;

            Assert.Throws<OutOfProgressHistoryBoundsException>(new Action(() => editableTask.Start = 101));
        }

        [Fact]
        public void DeleteATask()
        {
            var goalManager = new GoalManager(new TestGoalRepository());
            goalManager.Open(PATH_TO_FILE);

            var taskSummaries = goalManager.GetTaskSummaries();
            var numTasks = taskSummaries.Count;
            var firstTaskId = taskSummaries[0].Id;

            goalManager.DeleteTask(firstTaskId);

            Assert.Equal(numTasks - 1, goalManager.GetTaskSummaries().Count);
            Assert.Null(goalManager.GetTaskSummary(firstTaskId));
        }

        [Fact]
        public void SaveAGoal_CallsSaveOnRepository()
        {
            var mockRepository = new Mock<IGoalRepository>();
            
            var goalManager = new GoalManager(mockRepository.Object);
            goalManager.Open(PATH_TO_FILE);

            goalManager.Save();

            mockRepository.Verify(r => r.Save(It.IsAny<DataGoal>(), It.IsAny<string>()), Times.Once());
        }
    }
}
