using System;
using System.Collections.Generic;
using System.Text;

namespace SmartClient.Domain
{
    public class GoalTaskProgressSnapshot : IGoalTaskProgressSnapshot
    {
        public GoalTaskProgressSnapshot(string day, double value)
        {
            this.Day = DateTime.Parse(day);
            this.Value = value;
        }

        public DateTime Day { get; private set; }
        public double Value { get; set; }
    }
}
