using System;
using System.Collections.Generic;
using System.Text;

namespace SmartClient.Domain
{
    public interface IGoalTaskSummary
    {
        string GoalTitle { get; }
        string Id { get; }
        string Title { get; }
        int PercentProgress { get; }
    }
}
