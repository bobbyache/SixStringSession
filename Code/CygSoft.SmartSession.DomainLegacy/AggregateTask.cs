using CygSoft.SmartSession.Infrastructure;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.DomainLegacy
{
    public class AggregateTask : BaseTask
    {
        private List<BaseTask> tasks = new List<BaseTask>();
        private ProgressCalculator progressCalculator = new ProgressCalculator();

        public override double PercentCompleted() => progressCalculator.CalculateTotalProgress();

        public override int MinutesPracticed => throw new NotImplementedException();

        public override DateTime? StartDate => throw new NotImplementedException();

        internal void AddChildTask(BaseTask task)
        {
            tasks.Add(task);
            progressCalculator.Add(task as IWeightedEntity);
        }
    }
}
