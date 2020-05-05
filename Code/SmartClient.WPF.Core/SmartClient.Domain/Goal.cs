using SmartClient.Domain.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SmartClient.Domain
{
    public class Goal
    {
        private readonly IGoalRepository goalRepository;

        public Goal(IGoalRepository goalRepository)
        {
            this.goalRepository = goalRepository;
        }

        public IEnumerable<GoalTaskProgressSnapshot> GetTaskProgressSnapshots(string taskId)
        {
            throw new NotImplementedException();
        }

        public GoalSummary GetSummary()
        {
            throw new NotImplementedException();
        }

        public void UpdateTaskProgressSnapshot(string taskId, DateTime date, int value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GoalTaskSummary> GetTaskSummaries(string taskId)
        {
            throw new NotImplementedException();
        }

        public GoalTask GetTask(string taskId)
        {
            throw new NotImplementedException();
        }

        public void UpdateTask(GoalTask task)
        {
            throw new NotImplementedException();
        }

        public void CreateTask(GoalTask task)
        {
            throw new NotImplementedException();
        }

        public void DeleteTask(string taskId)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
