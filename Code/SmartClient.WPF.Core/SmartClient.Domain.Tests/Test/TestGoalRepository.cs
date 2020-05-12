using SmartClient.Domain.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartClient.Domain.Tests.Test
{

    public class TestDataGoal : IDataGoal
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public IList<IDataGoalTask> Tasks { get; set; }
        public double Weighting { get; set; }
    }

    public class TestDataGoalTask : IDataGoalTask
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public IList<IDataGoalTaskProgressSnapshot> ProgressHistory { get; set; }
        public double Start { get; set; }
        public double Target { get; set; }
        public double Weighting { get; set; }
    }

    public class TestDataGoalTaskProgressSnapshot : IDataGoalTaskProgressSnapshot
    {
        public string Day { get; set; }
        public double Value { get; set; }
    }


    public class TestGoalRepository : IGoalRepository
    {
        public IDataGoal Open(string filePath)
        {
            var dataGoal = new TestDataGoal();
            dataGoal.Id = "AD0FEA8A-87F4-4A86-B948-EE863405BFC6";
            dataGoal.Title = "Test Data Goal";
            dataGoal.Weighting = 0.5;

            dataGoal.Tasks = new List<IDataGoalTask>
            {
                GetDataGoalTask("9a3c801b-5e5c-423c-9696-6a2f687f31db", "Test Task 1"),
                GetDataGoalTask("8233815a-2fa8-435d-98da-b84f416604f7", "Test Task 2")
            };

            return dataGoal;
         }

        private TestDataGoalTask GetDataGoalTask(string id, string title)
        {
            var goalTask = new TestDataGoalTask();
            goalTask.Id = id;
            goalTask.Title = title;
            goalTask.Start = 0;
            goalTask.Target = 100;
            goalTask.Weighting = 0.5;

            var goalTaskProgressHistory = new List<IDataGoalTaskProgressSnapshot>
            {
                new TestDataGoalTaskProgressSnapshot() { Day = "2012-08-23", Value = 20 },
                new TestDataGoalTaskProgressSnapshot() { Day = "2012-09-05", Value = 20 },
                new TestDataGoalTaskProgressSnapshot() { Day = "2012-09-23", Value = 20 }
            };

            goalTask.ProgressHistory = goalTaskProgressHistory;

            return goalTask;
        }

        public void Save(IDataGoal goal, string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
