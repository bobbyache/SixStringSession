using SmartClient.Domain.Data;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace SmartClient.Domain
{
    public class GoalTaskSummary : IGoalTaskSummary
    {
        private readonly string goalTitle;
        private readonly DataGoalTask dataTask;

        public GoalTaskSummary(DataGoalTask dataTask, string goalTitle)
        {
            this.goalTitle = goalTitle;
            this.dataTask = dataTask;
        }

        public string GoalTitle => this.goalTitle;

        public string Id => dataTask.Id;

        public string Title => dataTask.Title;

        public int PercentProgress
        {
            get
            {
                return GetLatestProgressHistoryValue(this.dataTask.ProgressHistory);
            }
        }
        public double Weighting => dataTask.Weighting;

        public string UnitOfMeasure => dataTask.UnitOfMeasure;

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

