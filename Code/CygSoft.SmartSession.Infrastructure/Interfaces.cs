using System;

namespace CygSoft.SmartSession.Infrastructure
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
