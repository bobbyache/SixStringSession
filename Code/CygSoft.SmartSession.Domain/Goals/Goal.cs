using CygSoft.SmartSession.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.Domain.Goals
{
    public class Goal : EntityBase, IEditableGoal
    {
        private WeightingCalculator weightingCalculator;
        private List<IGoalFile> goalFiles = new List<IGoalFile>();
        private List<IEditableGoalTask> goalTasks = new List<IEditableGoalTask>();

        public string Title { get; set; }

        public double PercentComplete
        {
            get
            {
                double summedPercentage = 0;
                
                foreach (IWeightedEntity task in goalTasks)
                {
                    summedPercentage += weightingCalculator.GetItemWeightedPercentage(task.InstanceId, task.PercentCompleted);
                }
                return summedPercentage;
            }
        }

        public Goal()
        {

        }

        public Goal(int maxTaskWeighting)
        {
            weightingCalculator = new WeightingCalculator(maxTaskWeighting);
            CreateDate = DateTime.Now;
        }

        internal Goal(int maxTaskWeighting, IEditableGoalTask[] goalTasks, IGoalFile[] goalFiles)
        {
            weightingCalculator = new WeightingCalculator(maxTaskWeighting);

            if (goalTasks != null)
                this.goalTasks = new List<IEditableGoalTask>(goalTasks);

            if (goalFiles != null)
                this.goalFiles = new List<IGoalFile>(goalFiles);
        }

        public double FileCount => goalFiles.Count();

        private void GoalTask_WeightingChanged(object sender, EventArgs e)
        {
            //GoalTask goalTask = ((GoalTask)sender);
            IWeightedEntity goalTask = ((IWeightedEntity)sender);
            weightingCalculator.Update(goalTask.InstanceId, goalTask.Weighting);
        }

        public void AddTask(IEditableGoalTask goalTask)
        {
            IWeightedEntity weightedTask = goalTask as IWeightedEntity;

            if (weightedTask.Weighting <= 0)
                throw new ArgumentOutOfRangeException("Cannot add a task with an invalid weighting");

            weightingCalculator.Update(weightedTask.InstanceId, weightedTask.Weighting);
            goalTasks.Add(goalTask);
            weightedTask.WeightingChanged += GoalTask_WeightingChanged;
        }

        public IGoalFile[] Files => goalFiles.ToArray();
        public int MinutesPracticed { get; private set; }

        public DateTime CreateDate { get; private set; }

        public int Weighting => 0;

        public bool IsConsideredComplete => false;

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
