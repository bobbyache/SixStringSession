using SmartClient.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartClient.Domain.Tests.Test
{
    public class TestGoalRepository: IGoalRepository
    {
        public DataGoal Open(string filePath)
        {
            var dataGoal = new DataGoal();
            dataGoal.Id = "AD0FEA8A-87F4-4A86-B948-EE863405BFC6";
            dataGoal.Title = "Test Data Goal";
            dataGoal.Weighting = 0.5;

            dataGoal.Tasks = new List<DataGoalTask>
            {
                GetDataGoalTask("9a3c801b-5e5c-423c-9696-6a2f687f31db", "Test Task 1"),
                GetDataGoalTask("8233815a-2fa8-435d-98da-b84f416604f7", "Test Task 2")
            };

            return dataGoal;
         }

        private DataGoalTask GetDataGoalTask(string id, string title)
        {
            var goalTask = new DataGoalTask();
            goalTask.Id = id;
            goalTask.Title = title;
            goalTask.Start = 0;
            goalTask.Target = 100;
            goalTask.Weighting = 0.5;

            var goalTaskProgressHistory = new List<DataGoalTaskProgressSnapshot>
            {
                new DataGoalTaskProgressSnapshot() { Day = "2012-09-05", Value = 20 },
                new DataGoalTaskProgressSnapshot() { Day = "2012-09-23", Value = 20 },
                new DataGoalTaskProgressSnapshot() { Day = "2012-08-23", Value = 20 }
            };

            goalTask.ProgressHistory = goalTaskProgressHistory;

            return goalTask;
        }

        public void Save(DataGoal goal, string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
