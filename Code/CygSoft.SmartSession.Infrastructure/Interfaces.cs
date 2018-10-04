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

    public interface IEditableGoalTask
    {
        int Id { get; }
        string Title { get; }
        DateTime CreateDate { get; }
        int MinutesPracticed { get; }
        double PercentCompleted();
        DateTime? StartDate { get; }
    }

    public interface IWeightedEntity
    {
        int Weighting { get; set; }
        double PercentCompleted();
    }
}
