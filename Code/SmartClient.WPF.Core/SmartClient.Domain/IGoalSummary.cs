using System;
using System.Collections.Generic;
using System.Text;

namespace SmartClient.Domain
{
    public interface IGoalSummary
    {
        string Id { get; }
        string Title { get; set; }

        int PercentProgress { get; }
    }
}
