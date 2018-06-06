﻿using CygSoft.SmartSession.GoalManagement.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement.Tasks
{
    public class PercentGoalTask : GoalTask
    {
        public PercentGoalTask(string title) : base(title)
        {
        }

        public PercentGoalTask(string id, string title, DateTime createDate, List<PercentSessionResult> results)
            : base(id, title, createDate, results.OfType<SessionResult>().ToList())
        {
        }

        public override double PercentCompleted
        {
            get
            {
                if (sessionResults == null)
                    return 0;

                if (sessionResults.Count == 0)
                    return 0;

                return sessionResults.OfType<PercentSessionResult>().Max(r => r.PercentCompleted);
            }
        }
    }
}
