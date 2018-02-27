using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement
{
    public abstract class GoalTask
    {
        private int minutesRecorded;
        private int weighting;

        public int MinutesRecorded => minutesRecorded;
        public int Weighting => weighting;

        public GoalTask(int weighting, int minutesRecorded)
        {
            this.minutesRecorded = minutesRecorded;
            this.weighting = weighting;
        }

        public abstract int PercentCompleted { get; }
    }
}
