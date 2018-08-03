using CygSoft.SmartSession.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Tasks
{
    public class TaskMediator
    {
        private List<IWeightedEntity> weightedTasks = new List<IWeightedEntity>();

        public IEnumerable<IWeightedEntity> Tasks => weightedTasks;

        public double PercentComplete
        {
            get
            {
                double summedPercentage = 0;

                foreach (IWeightedEntity task in weightedTasks)
                {
                    //summedPercentage += weightingCalculator.GetItemWeightedPercentage(task.InstanceId, task.PercentCompleted);
                }
                return summedPercentage;
            }
        }

        public int TaskCount
        {
            get
            {
                if (weightedTasks != null)
                    return weightedTasks.Count();
                return 0;
            }
        }

        public void AddTask(IWeightedEntity weightedTask)
        {
            if (weightedTask.Weighting <= 0)
                throw new ArgumentOutOfRangeException("Cannot add a task with an invalid weighting");

            //weightingCalculator.Update(weightedTask.InstanceId, weightedTask.Weighting);
            weightedTasks.Add(weightedTask);
            //weightedTask.WeightingChanged += GoalTask_WeightingChanged;
        }
    }
}
