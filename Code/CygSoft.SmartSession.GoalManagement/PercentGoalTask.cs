using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement
{
    public class PercentGoalTask : GoalTask
    {
        public PercentGoalTask(string title) : base(title)
        {
        }

        public PercentGoalTask(string title, List<PercentSessionResult> results)
            : base(title, results.OfType<SessionResult>().ToList())
        {
        }

        public override double PercentCompleted => 0;
    }
}
