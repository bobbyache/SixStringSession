using CygSoft.SmartSession.DomainLegacy;
using CygSoft.SmartSession.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.Domain.Goals
{
    public class OldGoal : EntityBase, IEditableGoal
    {
        private List<IEditableGoalTask> goalTasks = new List<IEditableGoalTask>();
        private ProgressCalculator progressCalculator = new ProgressCalculator();

        public string Title { get; set; }
        public int MinutesPracticed { get; private set; }
        public DateTime CreateDate { get; private set; }
        public int Weighting => 0;
        public bool IsConsideredComplete => PercentComplete == 100;
        public double PercentComplete => progressCalculator.CalculateTotalProgress();
        public IEditableGoalTask[] Tasks => goalTasks.ToArray();

        public int TaskCount
        {
            get
            {
                if (goalTasks != null)
                    return goalTasks.Count();
                return 0;
            }
        }

        public OldGoal()
        {
            CreateDate = DateTime.Now;
        }

        internal OldGoal(IEditableGoalTask[] goalTasks)
        {
            if (goalTasks != null)
                this.goalTasks = new List<IEditableGoalTask>(goalTasks);
        }

        public void AddTask(IEditableGoalTask goalTask)
        {
            IWeightedEntity weightedTask = goalTask as IWeightedEntity;

            if (goalTask.PercentCompleted() < 0)
                throw new ArgumentOutOfRangeException("Percent cannot be a negative value.");

            if (goalTask.PercentCompleted() > 100)
                throw new ArgumentOutOfRangeException("Percent value cannot exceed 100.");

            if (weightedTask.Weighting <= 0)
                throw new ArgumentOutOfRangeException("Cannot add a task with an invalid weighting");

            if (weightedTask.Weighting > 100)
                throw new ArgumentOutOfRangeException("Cannot add a task with an invalid weighting");

            goalTasks.Add(goalTask);
            progressCalculator.Add(goalTask as IWeightedEntity);
        }
    }
}