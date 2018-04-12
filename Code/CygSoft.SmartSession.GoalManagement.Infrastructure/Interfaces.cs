using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement.Infrastructure
{
    public interface IGoalTask
    {
        DateTime CreateDate { get; }
        int MinutesPracticed { get; }
        int PercentCompleted { get; }
        DateTime? StartDate { get; }
        int Weighting { get; }
    }

    public interface IGoalFile
    {
    }
}
