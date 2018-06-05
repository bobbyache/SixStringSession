using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement
{
    public class DurationGoalTask : GoalTask
    {
        private string title;
        private List<DurationSessionResult> durationSessionResults;
        private int targetMinutes;

        public DurationGoalTask(string title) : base(title) { }

        public DurationGoalTask(string title, int targetMinutes, List<DurationSessionResult> durationSessionResults)
            : this(title)
        {
            this.title = title;
            this.targetMinutes = targetMinutes;
            this.durationSessionResults = durationSessionResults;
        }

        public override int PercentCompleted
        {
            get
            {
                if (durationSessionResults == null)
                    return 0;

                var minutes = durationSessionResults.Sum(t => t.Minutes);
                if (minutes > targetMinutes && targetMinutes != 0)
                    return 100;
                    //return (int)Math.Round(((double)minutes / targetMinutes) * 100);
                return (int)Math.Round(((double)minutes / targetMinutes) * 100);
            }
        }

        // eg. Hour, Minute.
        public string TimeUnit => "Hour";

        // eg. 6 hours.
        public int TargetUnit => 0;
    }
}
