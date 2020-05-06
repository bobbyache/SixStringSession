using SmartClient.Domain.Data;
using SmartClient.Domain.Weighting;
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

        public GoalSummary GetSummary()
        {
            var taskSummaries = GetTaskSummaries();

            WeightedProgressCalculator calculator = new WeightedProgressCalculator();
            calculator.AddRange(taskSummaries.ToList<IWeightedEntity>());
            var percentProgress = calculator.CalculateTotalProgress();
            return new GoalSummary(this.dataGoal.Id, this.dataGoal.Title, percentProgress);
        }

        public IList<IGoalTaskSummary> GetTaskSummaries()
        {
            var tasks = this.dataGoal.Tasks.Select(t => new GoalTaskSummary(
                    t.Id, t.Title, this.dataGoal.Title, (int)Math.Round(t.ProgressHistory.Last().Value), t.Weighting));
            return tasks.OfType<IGoalTaskSummary>().ToList();
        }

        public IGoalTaskSummary GetTaskSummary(string taskId)
        {
            var dataTask = this.dataGoal.Tasks.Where(t => t.Id == taskId).SingleOrDefault();
            var summary = new GoalTaskSummary(dataTask.Id, dataTask.Title, this.dataGoal.Title,
                (int)Math.Round(dataTask.ProgressHistory.Last().Value), dataTask.Weighting);
            return summary;
        }

        //public void UpdateTaskProgressSnapshot(string taskId, DateTime date, int value)
        //{
        //    throw new NotImplementedException();
        //}

        //public void UpdateTask(IEditableGoalTask task)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEditableGoalTask CreateTask()
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeleteTask(string taskId)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Save()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
