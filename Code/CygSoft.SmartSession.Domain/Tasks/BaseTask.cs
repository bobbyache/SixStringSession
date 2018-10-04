using CygSoft.SmartSession.Infrastructure;
using System;

namespace CygSoft.SmartSession.Domain.Tasks
{
    public abstract class BaseTask : EntityBase, IWeightedEntity, IEditableGoalTask
    {
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public abstract double PercentCompleted();
        public abstract int MinutesPracticed { get; }
        public int Weighting { get; set; }
        public abstract DateTime? StartDate { get; }

        public BaseTask()
        {
            CreateDate = DateTime.Now;
        }
    }
}
