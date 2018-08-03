using CygSoft.SmartSession.Domain.Tasks;
using CygSoft.SmartSession.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.Domain.Goals
{
    public class Goal : EntityBase, IEditableGoal
    {
        private List<IGoalFile> goalFiles = new List<IGoalFile>();
        private List<IEditableGoalTask> goalTasks = new List<IEditableGoalTask>();

        public string Title { get; set; }

        public double PercentComplete => progressCalculator.CalculateTotalProgress();

        public Goal()
        {
            CreateDate = DateTime.Now;
        }

        internal Goal(IEditableGoalTask[] goalTasks, IGoalFile[] goalFiles)
        {
            if (goalTasks != null)
                this.goalTasks = new List<IEditableGoalTask>(goalTasks);

            if (goalFiles != null)
                this.goalFiles = new List<IGoalFile>(goalFiles);
        }

        public double FileCount => goalFiles.Count();

        private void GoalTask_WeightingChanged(object sender, EventArgs e)
        {
            IWeightedEntity goalTask = ((IWeightedEntity)sender);;
        }

        private ProgressCalculator progressCalculator = new ProgressCalculator();

        public void AddTask(IEditableGoalTask goalTask)
        {
            IWeightedEntity weightedTask = goalTask as IWeightedEntity;

            if (goalTask.PercentCompleted < 0)
                throw new ArgumentOutOfRangeException("Percent cannot be a negative value.");

            if (goalTask.PercentCompleted > 100)
                throw new ArgumentOutOfRangeException("Percent value cannot exceed 100.");

            if (weightedTask.Weighting <= 0)
                throw new ArgumentOutOfRangeException("Cannot add a task with an invalid weighting");

            if (weightedTask.Weighting > 100)
                throw new ArgumentOutOfRangeException("Cannot add a task with an invalid weighting");

            goalTasks.Add(goalTask);
            progressCalculator.Add(goalTask as IWeightedEntity);
            weightedTask.WeightingChanged += GoalTask_WeightingChanged;
        }

        public IGoalFile[] Files => goalFiles.ToArray();
        public int MinutesPracticed { get; private set; }

        public DateTime CreateDate { get; private set; }

        public int Weighting => 0;

        public bool IsConsideredComplete => PercentComplete == 100;

        public int TaskCount
        {
            get
            {
                if (goalTasks != null)
                    return goalTasks.Count();
                return 0;
            }
        }

        public IEditableGoalTask[] Tasks => goalTasks.ToArray();
    }
}
