using System.Linq;

namespace CygSoft.SmartSession.DomainLegacy
{
    public class MetronomeGoalTask : GoalTask<MetronomeSessionResult>
    {
        public int TargetSpeed { get; set; }
        public int StartSpeed { get; set; }

        public int CurrentSpeed
        {
            get
            {
                if (sessionResultList == null)
                    return 0;

                if (sessionResultList.Count == 0)
                    return 0;

                return sessionResultList.OfType<MetronomeSessionResult>()
                    .OrderBy(r => r.StartTime).Last().Speed;
            }
        }

        public override double PercentCompleted()
        {
            if (StartSpeed > CurrentSpeed)
                return 0;

            int numerator = CurrentSpeed - StartSpeed;
            int denominator = TargetSpeed - StartSpeed;

            if (denominator > 0)
            {
                return ((double)numerator / denominator) * 100;
            }
            return 0;
        }
    }
}
