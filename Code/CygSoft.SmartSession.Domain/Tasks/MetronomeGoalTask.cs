using CygSoft.SmartSession.Domain.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.Domain.Tasks
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

                return sessionResultList.OfType<MetronomeSessionResult>().OrderBy(r => r.StartTime).Last().Speed;
            }
        }

        public override double PercentCompleted
        {
            get
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
}
