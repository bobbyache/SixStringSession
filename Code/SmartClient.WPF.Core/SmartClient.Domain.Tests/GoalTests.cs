using Moq;
using SmartClient.Domain.Data;
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
            var goal = MockHelpers.GetMockGoal();
            var taskSummary = goal.GetTaskSummary("9a3c801b-5e5c-423c-9696-6a2f687f31da");

            Assert.True(taskSummary != null);
            Assert.True(taskSummary.Id == "9a3c801b-5e5c-423c-9696-6a2f687f31da");
            Assert.Equal("Test Task 1", taskSummary.Title);
            Assert.True(taskSummary.PercentProgress == 75);
            Assert.True(taskSummary.GoalTitle == "Test Goal");
        }

        [Fact]
        public void GetAllTaskSummaries()
        {
            var goal = MockHelpers.GetMockGoal();
            var taskSummaries = goal.GetTaskSummaries();

            Assert.True(taskSummaries[0] != null);
            Assert.True(taskSummaries[0].Id == "9a3c801b-5e5c-423c-9696-6a2f687f31da");
            Assert.True(taskSummaries[0].Title == "Test Task 1");
            Assert.True(taskSummaries[0].PercentProgress == 75);
            Assert.True(taskSummaries[0].GoalTitle == "Test Goal");

            Assert.True(taskSummaries[1] != null);
            Assert.True(taskSummaries[1].Id == "9a3c801b-5e5c-423c-9696-6a2f687f31db");
            Assert.True(taskSummaries[1].Title == "Test Task 2");
            Assert.True(taskSummaries[1].PercentProgress == 90);
            Assert.True(taskSummaries[1].GoalTitle == "Test Goal");
        }

        [Fact]
        public void GetTaskProgressSnapshotsForTask()
        {
            var goal = MockHelpers.GetMockGoal();
            var goalSummary = goal.GetSummary();
            var snapshots = goal.GetTaskProgressSnapshots("9a3c801b-5e5c-423c-9696-6a2f687f31db");

            Assert.Equal(3, snapshots.Count());
            Assert.Equal(new DateTime(2010, 3, 15), snapshots[0].Day);
            Assert.Equal(10, snapshots[0].Value);
            Assert.Equal(new DateTime(2010, 5, 17), snapshots[2].Day);
        }

        [Fact]
        public void GetGoalSummary()
        {
            var goal = MockHelpers.GetMockGoal();

            var goalSummary = goal.GetSummary();
            Assert.Equal("8233815a-2fa8-435d-98da-b84f416604f7", goalSummary.Id);
            Assert.Equal("Test Goal", goalSummary.Title);
        }
    }
}
