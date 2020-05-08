using System;
using System.Collections.Generic;
using System.Text;

namespace SmartClient.Domain.Data
{
    public class DataGoalTask : IDataGoalTask
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public IList<IDataGoalTaskProgressSnapshot> ProgressHistory { get; set; } = new List<IDataGoalTaskProgressSnapshot>();
        public double Start { get; set; }
        public double Target { get; set; }
        public double Weighting { get; set; }
    }
}
