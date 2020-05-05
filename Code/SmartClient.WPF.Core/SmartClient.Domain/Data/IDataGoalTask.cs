using System;
using System.Collections.Generic;
using System.Text;

namespace SmartClient.Domain.Data
{
    public interface IDataGoalTask
    {
        string Id { get; set; }
        string Title { get; set; }
        IList<IDataGoalTaskProgressSnapshot> ProgressHistory { get; set; }
        double Start { get; set; }
        double Target { get; set; }

        double Weighting { get; set; }
    }
}
