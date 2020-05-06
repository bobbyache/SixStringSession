using Moq;
using SmartClient.Domain.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartClient.Domain.Tests
{
    public class MockHelpers
    {
        public static Goal GetMockGoal()
        {
            var taskMock1 = MockHelpers.GetGoalTaskMock("9a3c801b-5e5c-423c-9696-6a2f687f31da", "Test Task 1", 0, 100, 0.5,
                new List<IDataGoalTaskProgressSnapshot>
                {
                    MockHelpers.GetGoalTaskProgressSnapshotMock("2010-03-15", 25).Object,
                    MockHelpers.GetGoalTaskProgressSnapshotMock("2010-04-15", 50).Object,
                    MockHelpers.GetGoalTaskProgressSnapshotMock("2010-05-15", 75).Object
                }
            );

            var taskMock2 = MockHelpers.GetGoalTaskMock("9a3c801b-5e5c-423c-9696-6a2f687f31db", "Test Task 2", 0, 100, 0.5,
                new List<IDataGoalTaskProgressSnapshot>
                {
                    MockHelpers.GetGoalTaskProgressSnapshotMock("2010-03-15", 10).Object,
                    MockHelpers.GetGoalTaskProgressSnapshotMock("2010-04-15", 50).Object,
                    MockHelpers.GetGoalTaskProgressSnapshotMock("2010-05-17", 90).Object
                }
            );


            var goalMock = MockHelpers.GetGoalMock("8233815a-2fa8-435d-98da-b84f416604f7", "Test Goal", 0.5, new List<IDataGoalTask> { taskMock1.Object, taskMock2.Object });
            var goal = new Goal(MockHelpers.GetGoalRepositoryMock(goalMock).Object, null);

            return goal;
        }
        public static Mock<IGoalRepository> GetGoalRepositoryMock(Mock<IDataGoal> goalMock)
        {
            var repository = new Mock<IGoalRepository>();
            repository.Setup(r => r.Open(It.IsAny<string>())).Returns(goalMock.Object);

            return repository;
        }

        public static Mock<IDataGoal> GetGoalMock(string id, string title, double weighting, IList<IDataGoalTask> tasks)
        {
            var mock = new Mock<IDataGoal>();
            mock.Setup(g => g.Id).Returns(id);
            mock.Setup(g => g.Title).Returns(title);
            mock.Setup(g => g.Weighting).Returns(weighting);
            mock.Setup(g => g.Tasks).Returns(tasks);

            return mock;
        }
        public static Mock<IDataGoalTask> GetGoalTaskMock(string id, string title, int start, int target, double weighting, IList<IDataGoalTaskProgressSnapshot> progressSnapshots)
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

        public static Mock<IDataGoalTaskProgressSnapshot> GetGoalTaskProgressSnapshotMock(string day, double value)
        {
            var mock = new Mock<IDataGoalTaskProgressSnapshot>();
            mock.Setup(m => m.Day).Returns(day);
            mock.Setup(m => m.Value).Returns(value);

            return mock;
        }
    }
}
