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
        protected DataGoal dataGoal;
        protected readonly IGoalRepository goalRepository;
        private string filePath = null;

        public GoalManager(IGoalRepository goalRepository)
        {
            this.goalRepository = goalRepository;
        }

        protected bool IsOpened()
        {
            return this.filePath != null;
        }

        public void Open(string filePath)
        {
            this.filePath = filePath;
            this.dataGoal = goalRepository.Open(filePath);
        }

        private const string NO_DATA_LOADED = "No data loaded";

        public IList<IGoalTaskProgressSnapshot> GetTaskProgressSnapshots(string taskId)
        {
            if (!this.IsOpened()) throw new InvalidOperationException(NO_DATA_LOADED);

            var task = this.dataGoal.Tasks.Where(t => t.Id == taskId).SingleOrDefault();
            var taskProgressSnapshots = task.ProgressHistory.Select(ph => new GoalTaskProgressSnapshot(ph.Day, ph.Value)
                ).OfType<IGoalTaskProgressSnapshot>().ToList();

            return taskProgressSnapshots;
        }

        public IGoalSummary GetSummary()
        {
            if (!this.IsOpened()) throw new InvalidOperationException(NO_DATA_LOADED);

            return new GoalSummary(this.dataGoal);
        }

        public IGoalDetail GetDetail()
        {
            if (!this.IsOpened()) throw new InvalidOperationException(NO_DATA_LOADED);

            return new GoalDetail(this.dataGoal);
        }

        public IList<IGoalTaskSummary> GetTaskSummaries()
        {
            if (!this.IsOpened()) throw new InvalidOperationException(NO_DATA_LOADED);

            var tasks = this.dataGoal.Tasks.Select(t => new GoalTaskSummary(t, this.dataGoal.Title));
            return tasks.OfType<IGoalTaskSummary>().ToList();
        }

        public IGoalTaskSummary GetTaskSummary(string taskId)
        {
            if (!this.IsOpened()) throw new InvalidOperationException(NO_DATA_LOADED);

            var dataTask = this.dataGoal.Tasks.Where(t => t.Id == taskId).SingleOrDefault();
            if (dataTask != null)
            {
                var summary = new GoalTaskSummary(dataTask, this.dataGoal.Title);
                return summary;
            }
            return null;
        }

        private int GetLatestProgressHistoryValue(IList<DataGoalTaskProgressSnapshot> history)
        {
            if (!this.IsOpened()) throw new InvalidOperationException(NO_DATA_LOADED);

            if (history != null && history.Count() >= 1)
            {
                return (int)Math.Round(history.Last().Value);
            }
            return 0;
        }

        public IGoalTaskProgressSnapshot GetTaskProgressSnapshot(string taskId, DateTime date)
        {
            if (!this.IsOpened()) throw new InvalidOperationException(NO_DATA_LOADED);

            var dataTask = this.dataGoal.Tasks.Where(t => t.Id == taskId).SingleOrDefault();
            var snapshot = dataTask.ProgressHistory
                .Where(ph => ph.Day == date.ToString("yyyy-MM-dd")).SingleOrDefault();
            return new GoalTaskProgressSnapshot(snapshot.Day, snapshot.Value);
        }

        public void UpdateTaskProgressSnapshot(string taskId, DateTime date, int value)
        {
            if (!this.IsOpened()) throw new InvalidOperationException(NO_DATA_LOADED);

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

        public IEditableGoal GetEditableGoal()
        {
            if (!this.IsOpened()) throw new InvalidOperationException(NO_DATA_LOADED);

            return new EditableGoal(this.dataGoal);
        }

        public IEditableGoalTask CreateTask()
        {
            if (!this.IsOpened()) throw new InvalidOperationException(NO_DATA_LOADED);

            var dataGoalTask = new DataGoalTask();
            var editableGoalTask = new EditableGoalTask(this.dataGoal.Title, dataGoalTask);
            return editableGoalTask;
        }

        public void AddTask(IEditableGoalTask task)
        {
           var dataGoalTask =  ((EditableGoalTask)task).DataGoalTask;
            this.dataGoal.Tasks.Add(dataGoalTask);
        }

        public IEditableGoalTask GetEditableTask(string id)
        {
            if (!this.IsOpened()) throw new InvalidOperationException(NO_DATA_LOADED);

            var dataTask = this.dataGoal.Tasks.Where(t => t.Id == id).SingleOrDefault();
            var editableGoalTask = new EditableGoalTask(this.dataGoal.Title, dataTask);
            return editableGoalTask;
        }

        public void UpdateTask(IEditableGoalTask task)
        {
            if (!this.IsOpened()) throw new InvalidOperationException(NO_DATA_LOADED);

            throw new NotImplementedException();
        }

        public void DeleteTask(string taskId)
        {
            if (!this.IsOpened()) throw new InvalidOperationException(NO_DATA_LOADED);

            var dataTask = this.dataGoal.Tasks.Where(t => t.Id == taskId).SingleOrDefault();
            this.dataGoal.Tasks.Remove(dataTask);
        }

        public void Save()
        {
            if (!this.IsOpened()) throw new InvalidOperationException(NO_DATA_LOADED);

            this.goalRepository.Save(this.dataGoal, this.filePath);
        }

        public void Save(string filePath)
        {
            if (!this.IsOpened()) throw new InvalidOperationException(NO_DATA_LOADED);
            this.filePath = filePath;
            this.goalRepository.Save(this.dataGoal, this.filePath);
        }

        public GoalTaskDetail GetTaskDetail(string taskId)
        {
            if (!this.IsOpened()) throw new InvalidOperationException(NO_DATA_LOADED);

            var dataTask = this.dataGoal.Tasks.Where(t => t.Id == taskId).SingleOrDefault();
            return new GoalTaskDetail(dataTask, this.dataGoal.Title);
        }
    }
}
