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
        protected IDataGoal dataGoal;
        protected readonly IGoalRepository goalRepository;

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
                    t.Id, t.Title, this.dataGoal.Title, GetLatestProgressHistoryValue(t.ProgressHistory), t.Weighting));
            return tasks.OfType<IGoalTaskSummary>().ToList();
        }

        public IGoalTaskSummary GetTaskSummary(string taskId)
        {
            var dataTask = this.dataGoal.Tasks.Where(t => t.Id == taskId).SingleOrDefault();
            var summary = new GoalTaskSummary(dataTask.Id, dataTask.Title, this.dataGoal.Title,
                GetLatestProgressHistoryValue(dataTask.ProgressHistory), dataTask.Weighting);
            return summary;
        }

        private int GetLatestProgressHistoryValue(IList<IDataGoalTaskProgressSnapshot> history)
        {
            if (history != null && history.Count() >= 1)
            {
                return (int)Math.Round(history.Last().Value);
            }
            return 0;
        }

        public IGoalTaskProgressSnapshot GetTaskProgressSnapshot(string taskId, DateTime date)
        {
            var dataTask = this.dataGoal.Tasks.Where(t => t.Id == taskId).SingleOrDefault();
            var snapshot = dataTask.ProgressHistory
                .Where(ph => ph.Day == date.ToString("yyyy-MM-dd")).SingleOrDefault();
            return new GoalTaskProgressSnapshot(snapshot.Day, snapshot.Value);
        }

        public void UpdateTaskProgressSnapshot(string taskId, DateTime date, int value)
        {
            var dataTask = this.dataGoal.Tasks.Where(t => t.Id == taskId).SingleOrDefault();
            var snapshot = dataTask.ProgressHistory
                .Where(ph => ph.Day == date.ToString("yyyy-MM-dd")).SingleOrDefault();
            if (snapshot == null)
            {
                var newSnapshot = new DataGoalTaskProgressSnapshot()
                {
                    Day = date.ToString("yyyy-MM-dd"),
                    Value = value
                };
                dataTask.ProgressHistory.Add(newSnapshot);
            }
            else
            {
                snapshot.Value = value;
            }
        }

        public IEditableGoalTask CreateTask()
        {
            var dataGoalTask = new DataGoalTask();
            var editableGoalTask = new EditableGoalTask(this.dataGoal.Title, dataGoalTask);
            this.dataGoal.Tasks.Add(dataGoalTask);
            return editableGoalTask;
        }

        public IEditableGoalTask GetEditableTask(string id)
        {
            var dataTask = this.dataGoal.Tasks.Where(t => t.Id == id).SingleOrDefault();
            var editableGoalTask = new EditableGoalTask(this.dataGoal.Title, dataTask);
            return editableGoalTask;
        }

        public void UpdateTask(IEditableGoalTask task)
        {
            throw new NotImplementedException();
        }

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
