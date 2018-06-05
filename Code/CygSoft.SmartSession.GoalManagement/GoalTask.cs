using CygSoft.SmartSession.GoalManagement.Infrastructure;
using System;

namespace CygSoft.SmartSession.GoalManagement
{
    public abstract class GoalTask : IGoalTask
    {
        public int MinutesPracticed => 0;

        public double Weighting { get; internal set; }

        public DateTime CreateDate { get; private set; }

        // inferred by whatever the first session result is...
        public DateTime? StartDate => null;

        public GoalTask(string title)
        {
            Id = Guid.NewGuid().ToString();
            CreateDate = DateTime.Now;
            Title = title;
        }

        public abstract double PercentCompleted { get; }

        public string Title { get; private set; }

        public string Id { get; private set; }
    }
}
