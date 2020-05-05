using System;
using System.Collections.Generic;
using System.Text;

namespace SmartClient.Domain.Data
{
    public interface IDataGoal
    {
        string Id { get; set; }
        string Title { get; set; }
        IList<IDataGoalTask> Tasks { get; set; }
        double Weighting { get; set; }
    }
}
