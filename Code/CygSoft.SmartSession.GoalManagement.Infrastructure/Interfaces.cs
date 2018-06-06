using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement.Infrastructure
{
    public interface IGoalTask : IWeightedEntity
    {
        string Id { get; }
        string Title { get; }
        DateTime CreateDate { get; }
        int MinutesPracticed { get; }
        double PercentCompleted { get; }
        DateTime? StartDate { get; }
    }

    public interface IWeightedEntity
    {
        event EventHandler WeightingChanged;

        int Weighting { get; set; }
    }

    public interface IGoalFile
    {
    }
}
