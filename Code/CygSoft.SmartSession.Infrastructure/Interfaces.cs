using System;

namespace CygSoft.SmartSession.Infrastructure
{
    public interface IEditableGoal
    {
        int Id { get; }
        string Title { get; set; }

        int MinutesPracticed { get; }

        DateTime CreateDate { get; }

        int Weighting { get; }

        bool IsConsideredComplete { get; }

        void AddTask(IEditableGoalTask goalTask);
    }

    public interface IEditableGoalTask : IWeightedEntity
    {
        int Id { get; }
        string InstanceId { get; }
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
