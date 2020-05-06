using SmartClient.Domain.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SmartClient.Domain
{
    public class GoalManager
    {
        private IDataGoal dataGoal;
        private readonly IGoalRepository goalRepository;

        public GoalManager(IGoalRepository goalRepository, string filePath)
        {
            this.goalRepository = goalRepository;
            this.dataGoal = goalRepository.Open(filePath);
        }

        public IList<IGoalTaskProgressSnapshot> GetTaskProgressSnapshots(string taskId)
        {
            var task = this.dataGoal.Tasks.Where(t => t.Id == taskId).SingleOrDefault();
            var taskProgressSnapshots = task.ProgressHistory.Select(ph => new GoalTaskProgressSnapshot(ph.Day, ph.Value)
                ).OfType<IGoalTaskProgressSnapshot>().ToList();

            return taskProgressSnapshots;
        }

        // TODO: Get Summary
        public GoalSummary GetSummary()
        {
            return new GoalSummary(this.dataGoal.Id, this.dataGoal.Title);
        }

        public void UpdateTaskProgressSnapshot(string taskId, DateTime date, int value)
        {
            throw new NotImplementedException();
        }


        // TODO: Get the task (but why... for reading or for viewing?
        public GoalTask GetTask(string taskId)
        {
            throw new NotImplementedException();
        }

        public IList<IGoalTaskSummary> GetTaskSummaries()
        {
            var tasks = this.dataGoal.Tasks.Select(t => new GoalTaskSummary(
                    t.Id, t.Title, this.dataGoal.Title, (int)Math.Round(t.ProgressHistory.Last().Value
                )));
            return tasks.OfType<IGoalTaskSummary>().ToList();
        }

        public IGoalTaskSummary GetTaskSummary(string taskId)
        {
            var dataTask = this.dataGoal.Tasks.Where(t => t.Id == taskId).SingleOrDefault();
            var summary = new GoalTaskSummary(dataTask.Id, dataTask.Title, this.dataGoal.Title,
                (int)Math.Round(dataTask.ProgressHistory.Last().Value)
                );
            return summary;
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
