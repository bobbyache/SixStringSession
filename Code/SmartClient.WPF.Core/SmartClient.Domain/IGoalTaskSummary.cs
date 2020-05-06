using SmartClient.Domain.Weighting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartClient.Domain
{
    public interface IGoalTaskSummary: IWeightedEntity
    {
        string GoalTitle { get; }
        string Id { get; }
        string Title { get; }
    }
}
