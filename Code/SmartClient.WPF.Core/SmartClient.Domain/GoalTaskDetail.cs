using SmartClient.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartClient.Domain
{
    public class GoalTaskDetail : IGoalTaskDetail
    {
        private DataGoalTask task;

        public GoalTaskDetail(DataGoalTask task)
        {
            this.task = task;
        }

        public string Id => task.Id;

        public string Title => task.Title;

        public double Weighting => task.Weighting;

        public double Start => task.Start;

        public double Target => task.Target;

        public int PercentProgress => throw new NotImplementedException();

        public IGoalTaskProgressSnapshot[] TaskProgressSnapshots
        {
            get
            {
                var history = task.ProgressHistory.Select(item => new GoalTaskProgressSnapshot(item.Day, item.Value));
                return history.OrderBy(h => h.Day).ToArray();
            }
        }
    }
}
