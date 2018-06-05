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
        private int targetMinutes;

        public DurationGoalTask(string title) : base(title) { }

        public DurationGoalTask(string title, int targetMinutes, List<DurationSessionResult> results)
            : base(title, results.OfType<SessionResult>().ToList())
        {
            this.title = title;
            this.targetMinutes = targetMinutes;
        }

        public override double PercentCompleted
        {
            get
            {
                if (results == null)
                    return 0;

                var minutes = results.Sum(t => t.Minutes);
                if (minutes > targetMinutes && targetMinutes != 0)
                    return 100;

                return ((double)minutes / targetMinutes) * 100;
            }
        }
    }
}
