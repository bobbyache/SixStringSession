using System;
using System.Collections.Generic;

namespace SmartSession.Domain
{
    public class Goal
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public IList<GoalTask> Tasks { get; set; } = new List<GoalTask>();

        public double Weighting { get; set; } = 0.5;

        public double GetPercentComplete()
        {
            var calculator = new WeightedProgressCalculator();

            foreach (var task in Tasks)
            {
                calculator.Add(task);
            }
            return calculator.CalculateTotalProgress();
        }
    }
}
