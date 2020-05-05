using System;
using System.Collections.Generic;
using System.Text;

namespace SmartClient.Domain.Data
{
    public interface IDataGoalTaskProgressSnapshot
    {
        string Day { get; set; }
        double Value { get; set; }
    }
}
