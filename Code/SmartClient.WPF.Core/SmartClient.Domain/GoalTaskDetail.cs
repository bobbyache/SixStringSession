using SmartClient.Domain.Common;
using SmartClient.Domain.Data;
using SmartClient.Domain.Weighting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace SmartClient.Domain
{
    public class GoalTaskDetail : IGoalTaskDetail
    {
        private DataGoalTask task;

        public GoalTaskDetail(DataGoalTask task, string goalTitle)
        {
            this.task = task;
            this.GoalTitle = goalTitle;
        }

        public string Id => task.Id;

        public string Title => task.Title;

        public string Type => task.UnitOfMeasure;

        public double Weighting => task.Weighting;

        public double Start => task.Start;

        public double Target => task.Target;

        public int PercentProgress
        {
            get
            {
                return GetLatestProgressHistoryValue(task.ProgressHistory);
            }
        }

        public IGoalTaskProgressSnapshot[] TaskProgressSnapshots
        {
            get
            {
                var history = task.ProgressHistory.Select(item => new GoalTaskProgressSnapshot(item.Day, item.Value));
                return history.OrderBy(h => h.Day).ToArray();
            }
        }

        public TaskUnitOfMeasure UnitOfMeasure => Utils.ParseToEnum<TaskUnitOfMeasure>(this.task.UnitOfMeasure);

        public string GoalTitle { get; }

        private int GetLatestProgressHistoryValue(IList<DataGoalTaskProgressSnapshot> history)
        {
            if (history != null && history.Count() >= 1)
            {
                return (int)Math.Round(history.Last().Value);
            }
            return 0;
        }

    }
}
