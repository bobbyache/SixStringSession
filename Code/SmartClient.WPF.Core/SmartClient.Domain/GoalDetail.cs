using SmartClient.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartClient.Domain
{
    public class GoalDetail : GoalSummary, IGoalDetail
    {
        public GoalDetail(DataGoal dataGoal) : base(dataGoal) { }
        public IGoalTaskSummary[] TaskSummaries
        {
            get
            {
                return base.taskSummaries.ToArray();
            }
        }
    }
}
