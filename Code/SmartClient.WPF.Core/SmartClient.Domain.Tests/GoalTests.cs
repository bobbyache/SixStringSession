using Moq;
using SmartClient.Domain.Data;
using SmartClient.Domain.Tests.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartClient.Domain.Tests
{
    // InternalsVisibleTo in a .net CORE project
    // https://www.meziantou.net/declaring-internalsvisibleto-in-the-csproj.htm
    public class GoalTests
    {
        [Fact]
        public void GetATaskSummaryGivenATaskId()
        {
            var goalManager = MockHelpers.GetMockGoalManager();
            var taskSummary = goalManager.GetTaskSummary("9a3c801b-5e5c-423c-9696-6a2f687f31da");

            Assert.True(taskSummary != null);
            Assert.True(taskSummary.Id == "9a3c801b-5e5c-423c-9696-6a2f687f31da");
            Assert.Equal("Test Task 1", taskSummary.Title);
            Assert.True(taskSummary.PercentProgress == 75);
            Assert.True(taskSummary.Weighting == 0.5);
            Assert.True(taskSummary.GoalTitle == "Test Goal");
        }

        [Fact]
        public void GetAllTaskSummaries()
        {
            var goalManager = MockHelpers.GetMockGoalManager();
            var taskSummaries = goalManager.GetTaskSummaries();

            Assert.True(taskSummaries[0] != null);
            Assert.True(taskSummaries[0].Id == "9a3c801b-5e5c-423c-9696-6a2f687f31da");
            Assert.True(taskSummaries[0].Title == "Test Task 1");
            Assert.True(taskSummaries[0].PercentProgress == 75);
            Assert.True(taskSummaries[0].Weighting == 0.5);
            Assert.True(taskSummaries[0].GoalTitle == "Test Goal");

            Assert.True(taskSummaries[1] != null);
            Assert.True(taskSummaries[1].Id == "9a3c801b-5e5c-423c-9696-6a2f687f31db");
            Assert.True(taskSummaries[1].Title == "Test Task 2");
            Assert.True(taskSummaries[1].PercentProgress == 90);
            Assert.True(taskSummaries[1].Weighting == 0.5);
            Assert.True(taskSummaries[1].GoalTitle == "Test Goal");
        }

        [Fact]
        public void GetTaskProgressSnapshotsForTask()
        {
            var goalManager = MockHelpers.GetMockGoalManager();
            var goalSummary = goalManager.GetSummary();
            var snapshots = goalManager.GetTaskProgressSnapshots("9a3c801b-5e5c-423c-9696-6a2f687f31db");

            Assert.Equal(3, snapshots.Count());
            Assert.Equal(new DateTime(2010, 3, 15), snapshots[0].Day);
            Assert.Equal(10, snapshots[0].Value);
            Assert.Equal(new DateTime(2010, 5, 17), snapshots[2].Day);
        }

        [Fact]
        public void GetGoalSummary()
        {
            var goalManager = MockHelpers.GetMockGoalManager();

            var goalSummary = goalManager.GetSummary();
            Assert.Equal("8233815a-2fa8-435d-98da-b84f416604f7", goalSummary.Id);
            Assert.Equal("Test Goal", goalSummary.Title);
            Assert.Equal(82, goalSummary.PercentProgress);
        }

        [Fact]
        public void GetTaskProgressSnapshot()
        {
            var goalManager = MockHelpers.GetMockGoalManager();
            var progressSnapshot = goalManager.GetTaskProgressSnapshot("9a3c801b-5e5c-423c-9696-6a2f687f31db", DateTime.Parse("2010-04-15 10:22:22"));
            Assert.NotNull(progressSnapshot);
        }


        [Fact]
        public void UpdateTaskProgressSnapshot()
        {
            var goalManager = new GoalManager(new TestGoalRepository(), string.Empty);
            goalManager.UpdateTaskProgressSnapshot("9a3c801b-5e5c-423c-9696-6a2f687f31db", DateTime.Parse("2012-09-05 11:51:38"), 6);
            var snapshot = goalManager.GetTaskProgressSnapshot("9a3c801b-5e5c-423c-9696-6a2f687f31db", DateTime.Parse("2012-09-05 10:22:22"));

            Assert.Equal(6, snapshot.Value);
        }

        [Fact]
        public void CreateATask()
        {
            var goalManager = new GoalManager(new TestGoalRepository(), string.Empty);
            var editableTask = goalManager.CreateTask();

            Assert.NotNull(editableTask);

            Guid guidResult;
            Assert.True(Guid.TryParse(editableTask.Id, out guidResult), "Expected that ID would be a type of GUID");
            Assert.Equal("New Task", editableTask.Title);
            Assert.Equal(0.5, editableTask.Weighting);
        }

        [Fact]
        public void CreateATask_CreatesATaskOnGoal()
        {
            var goalManager = new GoalManager(new TestGoalRepository(), string.Empty);
            var editableTask = goalManager.CreateTask();
            var taskSummary = goalManager.GetTaskSummary(editableTask.Id);

            Assert.NotNull(taskSummary);

            Guid guidResult;
            Assert.True(Guid.TryParse(editableTask.Id, out guidResult), "Expected that ID would be a type of GUID");

            Assert.Equal(0, taskSummary.PercentProgress);
            Assert.Equal(0.5, taskSummary.Weighting);
            Assert.Equal("New Task", taskSummary.Title);
            Assert.Equal(goalManager.GetSummary().Title, taskSummary.GoalTitle);
        }
    }
}
