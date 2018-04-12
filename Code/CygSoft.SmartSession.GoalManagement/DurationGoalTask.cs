using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement
{
    public class DurationGoalTask : GoalTask
    {
        public DurationGoalTask(string title) : base(title)
        {

        }

        public override int PercentCompleted => 0;

        // eg. Hour, Minute.
        public string TimeUnit => "Hour";

        // eg. 6 hours.
        public int TargetUnit => 0;
    }
}
