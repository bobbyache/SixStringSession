using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement
{
    public class PercentDrivenTask : GoalTask
    {
        int percentComplete = 0;

        public PercentDrivenTask(int weighting, int minutesRecorded, int percentProgress)
            : base(weighting, minutesRecorded)
        {
            this.percentComplete = percentProgress;
        }

        public override int PercentCompleted => percentComplete;
    }
}
