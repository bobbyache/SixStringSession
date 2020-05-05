using Moq;
using SmartClient.Domain.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartClient.Domain.Tests
{
    // InternalsVisibleTo in a .net CORE project
    // https://www.meziantou.net/declaring-internalsvisibleto-in-the-csproj.htm
    public class GoalTests
    {
        [Fact]
        public void Test1()
        {
            var taskMock = GetGoalTaskMock("9a3c801b-5e5c-423c-9696-6a2f687f31da", "Test Task", 0, 100, 0.5,
                new List<IDataGoalTaskProgressSnapshot>
                {
                    GetGoalTaskProgressSnapshotMock("2010-03-15", 25).Object,
                    GetGoalTaskProgressSnapshotMock("2010-04-15", 50).Object,
                    GetGoalTaskProgressSnapshotMock("2010-05-15", 75).Object
                }
            );

            var goalMock = GetGoalMock("8233815a-2fa8-435d-98da-b84f416604f7", "Test Goal", 0.5, new List<IDataGoalTask> { taskMock.Object });

            var repository = new Mock<IGoalRepository>();
            repository.Setup(r => r.Open(It.IsAny<string>())).Returns(goalMock.Object);

            var goal = new Goal(repository.Object);

            Assert.True(goal.GetTask("9a3c801b-5e5c-423c-9696-6a2f687f31da") != null);
        }

        private Mock<IDataGoal> GetGoalMock(string id, string title, double weighting, IList<IDataGoalTask> tasks)
        {
            var mock = new Mock<IDataGoal>();
            mock.Setup(g => g.Id).Returns(id);
            mock.Setup(g => g.Title).Returns(title);
            mock.Setup(g => g.Weighting).Returns(weighting);
            mock.Setup(g => g.Tasks).Returns(tasks);

            return mock;
        }
        private Mock<IDataGoalTask> GetGoalTaskMock(string id, string title, int start, int target, double weighting, IList<IDataGoalTaskProgressSnapshot> progressSnapshots)
        {
            var mock = new Mock<IDataGoalTask>();
            mock.Setup(t => t.Id).Returns(id);
            mock.Setup(t => t.Title).Returns(title);
            mock.Setup(t => t.Start).Returns(start);
            mock.Setup(t => t.Target).Returns(target);
            mock.Setup(t => t.Weighting).Returns(weighting);
            mock.Setup(t => t.ProgressHistory).Returns(progressSnapshots);

            return mock;
        }

        private Mock<IDataGoalTaskProgressSnapshot> GetGoalTaskProgressSnapshotMock(string day, double value)
        {
            var mock = new Mock<IDataGoalTaskProgressSnapshot>();
            mock.Setup(m => m.Day).Returns(day);
            mock.Setup(m => m.Value).Returns(value);

            return mock;
        }
    }
}
