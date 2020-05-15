using SmartClient.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartClient.Domain
{
    public interface IEditableGoalTask
    {
        string GoalTitle { get; }
        string Id { get; }

        string Title { get; set; }
        TaskUnitOfMeasure UnitOfMeasure { get; set; }
        public double Weighting { get; set; }
        public double Start { get; set; }
        public double Target { get; set; }
    }
}
