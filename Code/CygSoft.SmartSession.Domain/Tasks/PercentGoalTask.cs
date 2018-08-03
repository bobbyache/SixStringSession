using CygSoft.SmartSession.Domain.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Tasks
{
    public class PercentGoalTask : GoalTask<PercentSessionResult>
    {
        //public PercentGoalTask(string title) : base(title)
        //{
        //}

        //public PercentGoalTask(string title, DateTime createDate, List<PercentSessionResult> results)
        //    : base(title, createDate, results.OfType<SessionResult>().ToList())
        //{
        //}

        public override double PercentCompleted
        {
            get
            {
                if (sessionResultList == null)
                    return 0;

                if (sessionResultList.Count == 0)
                    return 0;

                return sessionResultList.OfType<PercentSessionResult>().Max(r => r.PercentCompleted);
            }
        }
    }
}
