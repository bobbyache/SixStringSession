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

        public double Weighting { get; set; }
    }
}
