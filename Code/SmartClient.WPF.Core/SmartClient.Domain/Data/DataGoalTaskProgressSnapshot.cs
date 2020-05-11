using System;
using System.Collections.Generic;
using System.Text;

namespace SmartClient.Domain.Data
{
    public class DataGoalTaskProgressSnapshot : IDataGoalTaskProgressSnapshot
    {
        public string Day { get; set; }
        public double Value { get; set; }
    }
}
