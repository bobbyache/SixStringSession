using CygSoft.SmartSession.DomainLegacy;
using NUnit.Framework;
using System;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    class AggregateGoalTaskTests
    {
        [Test]
        public void AggregateTask_Calculates_PercentComplete_Correctly_If_All_ChildTask_Weightings_Equal()
        {
            AggregateTask aggGoalTask = new AggregateTask();

            aggGoalTask.AddChildTask(CreatePercentGoalTask(percentCompleted:25, weighting:100));
            aggGoalTask.AddChildTask(CreatePercentGoalTask(percentCompleted:50, weighting: 100));
            aggGoalTask.AddChildTask(CreatePercentGoalTask(percentCompleted:75, weighting: 100));

            Assert.That(aggGoalTask.PercentCompleted, Is.InRange(49.9, 50));
        }

        [Test]
        public void AggregateTask_Calculates_PercentComplete_Correctly_If_All_ChildTask_Weightings_Equal_And_One_Is_Aggregate_Task()
        {
            AggregateTask childAggregateTask = new AggregateTask();
            childAggregateTask.Weighting = 100;

            childAggregateTask.AddChildTask(CreatePercentGoalTask(percentCompleted: 25, weighting: 100));
            childAggregateTask.AddChildTask(CreatePercentGoalTask(percentCompleted: 50, weighting: 100));
            childAggregateTask.AddChildTask(CreatePercentGoalTask(percentCompleted: 75, weighting: 100));

            AggregateTask parentAggregateTask = new AggregateTask();
            
            parentAggregateTask.AddChildTask(childAggregateTask);
            parentAggregateTask.AddChildTask(CreatePercentGoalTask(percentCompleted: 50, weighting: 100));

            Assert.That(parentAggregateTask.PercentCompleted, Is.InRange(49.9, 50));
        }

        private PercentGoalTask CreatePercentGoalTask(double percentCompleted, int weighting)
        {
            PercentGoalTask task = new PercentGoalTask();
            task.AddSession(new PercentSessionResult(DateTime.Now.Subtract(new TimeSpan(0, 5, 0)), DateTime.Now, percentCompleted));
            task.Weighting = weighting;

            return task;
        }
    }
}
