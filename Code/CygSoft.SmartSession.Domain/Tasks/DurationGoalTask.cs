using CygSoft.SmartSession.Domain.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Tasks
{
    public class DurationGoalTask : GoalTask<DurationSessionResult>
    {
        public int TargetMinutes { get; set; }

        //public DurationGoalTask(string title, DateTime createDate, int targetMinutes, List<DurationSessionResult> results)
        //    : base(title, createDate, results.OfType<SessionResult>().ToList())
        //{
        //    this.title = title;
        //    this.targetMinutes = targetMinutes;
        //}

        public override double PercentCompleted
        {
            get
            {
                if (sessionResultList == null)
                    return 0;

                var minutes = sessionResultList.Sum(t => t.Minutes);
                if (minutes > TargetMinutes && TargetMinutes != 0)
                    return 100;

                return ((double)minutes / TargetMinutes) * 100;
            }
        }
    }
}
