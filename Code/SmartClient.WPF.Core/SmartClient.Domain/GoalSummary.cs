using SmartClient.Domain.Data;
using SmartClient.Domain.Weighting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartClient.Domain
{
    public class GoalSummary : IGoalSummary
    {
        protected IList<IGoalTaskSummary> taskSummaries;
        public GoalSummary(DataGoal dataGoal)
        {
            this.Id = dataGoal.Id;
            this.Title = dataGoal.Title;
            this.taskSummaries = GetTaskSummaries(dataGoal);
            this.PercentProgress = GetPercentProgress();

        }
        public string Id { get; private set; }

        public string Title { get; private set; }

        public int PercentProgress { get; private set; }

        private int GetPercentProgress()
        {
            WeightedProgressCalculator calculator = new WeightedProgressCalculator();
            calculator.AddRange(this.taskSummaries.ToList<IWeightedEntity>());
            var percentProgress = calculator.CalculateTotalProgress();

            return percentProgress;
        }

        private IList<IGoalTaskSummary> GetTaskSummaries(DataGoal dataGoal)
        {
            var tasks = dataGoal.Tasks.Select(t => new GoalTaskSummary(
                    t.Id, t.Title, dataGoal.Title, GetLatestProgressHistoryValue(t.ProgressHistory), t.Weighting));
            return tasks.OfType<IGoalTaskSummary>().ToList();
        }

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