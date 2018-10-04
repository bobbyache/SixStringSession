using System.Linq;

namespace CygSoft.SmartSession.DomainLegacy
{
    public class PercentGoalTask : GoalTask<PercentSessionResult>
    {
        public override double PercentCompleted()
        {
            if (sessionResultList == null)
                return 0;

            if (sessionResultList.Count == 0)
                return 0;

            return sessionResultList.OfType<PercentSessionResult>()
                .Max(r => r.PercentCompleted);
        }
    }
}
