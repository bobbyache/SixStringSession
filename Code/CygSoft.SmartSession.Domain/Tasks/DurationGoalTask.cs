using CygSoft.SmartSession.Domain.Sessions;
using System.Linq;

namespace CygSoft.SmartSession.Domain.Tasks
{
    public class DurationGoalTask : GoalTask<DurationSessionResult>
    {
        public int TargetMinutes { get; set; }

        public override double PercentCompleted()
        {
            if (sessionResultList == null)
                return 0;

            var minutes = sessionResultList.Sum(t => t.Minutes);
            if (minutes > TargetMinutes && TargetMinutes != 0)
                return 100;

            return ((double)minutes / TargetMinutes) * 100;
        }
    }
}
