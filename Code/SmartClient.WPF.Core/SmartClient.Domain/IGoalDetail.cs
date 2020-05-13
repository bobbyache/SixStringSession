using System;
using System.Collections.Generic;
using System.Text;

namespace SmartClient.Domain
{
    public interface IGoalDetail : IGoalSummary
    {
        IGoalTaskSummary[] TaskSummaries { get; }
    }
}
