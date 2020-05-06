using System;
using System.Collections.Generic;
using System.Text;

namespace SmartClient.Domain
{
    public interface IGoalTaskProgressSnapshot
    {
        DateTime Day { get; }
        double Value { get; set; }
    }
}
