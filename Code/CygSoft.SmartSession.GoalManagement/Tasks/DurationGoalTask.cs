using CygSoft.SmartSession.GoalManagement.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement.Tasks
{
    public class DurationGoalTask : GoalTask
    {
        private string title;
        private int targetMinutes;

        public DurationGoalTask(string title, int targetMinutes) : base(title)
        {
            this.targetMinutes = targetMinutes;
        }

        public DurationGoalTask(string title, DateTime createDate, int targetMinutes, List<DurationSessionResult> results)
            : base(title, createDate, results.OfType<SessionResult>().ToList())
        {
            this.title = title;
            this.targetMinutes = targetMinutes;
        }

        public override double PercentCompleted
        {
            get
            {
                if (sessionResults == null)
                    return 0;

                var minutes = sessionResults.Sum(t => t.Minutes);
                if (minutes > targetMinutes && targetMinutes != 0)
                    return 100;

                return ((double)minutes / targetMinutes) * 100;
            }
        }
    }
}
