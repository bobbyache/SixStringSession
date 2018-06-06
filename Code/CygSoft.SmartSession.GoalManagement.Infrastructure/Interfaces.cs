using System;

namespace CygSoft.SmartSession.GoalManagement.Infrastructure
{
    public interface IGoalSerializer
    {
        string Serialize(IGoal goal);
        IGoal Deserialize(string serializedGoal);
    }

    public interface IGoal
    {
        string Id { get; }
        string Title { get; set; }
        DateTime CreateDate { get; }
        double FileCount { get; }
        IGoalFile[] Files { get; }
        bool IsConsideredComplete { get; }
        int MinutesPracticed { get; }
        double PercentComplete { get; }
        int TaskCount { get; }
        IGoalTask[] Tasks { get; }
        int Weighting { get; }
        int MaxTaskWeighting { get; }
    }

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
