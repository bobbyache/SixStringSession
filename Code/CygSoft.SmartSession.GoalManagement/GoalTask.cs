using CygSoft.SmartSession.GoalManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement
{
    public abstract class GoalTask : IGoalTask
    {
        public int MinutesPracticed => 0;

        public int Weighting => 0;

        public DateTime CreateDate { get; private set; }

        // inferred by whatever the first session result is...
        public DateTime? StartDate => null;

        public GoalTask()
        {
            CreateDate = DateTime.Now;
        }

        public abstract int PercentCompleted { get; }
    }
}
