using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartSession.Domain
{
    public class GoalTask : IWeightedEntity
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public double Start { get; set; }

        public double Target { get; set; }

        public IList<GoalTaskHistoryItem> History { get; set; } = new List<GoalTaskHistoryItem>();

        public double Weighting { get; set; } = 0.5;

        public double PercentCompleted()
        {
            var numerator = (double)(GetLatestValue() - Start);
            var denominator = (double)(Target - Start);

            if (denominator == 0)
            {
                return 0;
            }
            else
            {
                var percentComplete = (numerator / denominator) * 100d;
                return percentComplete > 100 ? 100 : percentComplete;
            }
        }

        private double GetLatestValue()
        {
            if (!History.Any())
                return Start;

            // get the last record, get the speed.
            var lastActivityDate = History.Max(a => a.Date);
            var manualProgress = History.Where(a => a.Date == lastActivityDate).Select(a => a.Value).Last();
            return manualProgress;
        }
    }
}
