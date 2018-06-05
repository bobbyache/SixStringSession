using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.GoalManagement
{
    public class MetronomeGoalTask : GoalTask
    {
        private int targetSpeed;
        private int startSpeed;

        public MetronomeGoalTask(string title) : base(title)
        {
        }

        public MetronomeGoalTask(string title, DateTime createDate, int startSpeed, int targetSpeed, List<MetronomeSessionResult> results) 
            : base(title, createDate, results.OfType<SessionResult>().ToList())
        {
            if (startSpeed > targetSpeed)
                throw new ArgumentOutOfRangeException("Start metronome speed cannot exceed the target metronome speed.");

            this.targetSpeed = targetSpeed;
            this.startSpeed = startSpeed;
        }

        public int CurrentSpeed
        {
            get
            {
                if (results == null)
                    return 0;

                if (results.Count == 0)
                    return 0;

                return results.OfType<MetronomeSessionResult>().OrderBy(r => r.StartTime).Last().Speed;
            }
        }
        public int TargetSpeed => targetSpeed;

        public override double PercentCompleted
        {
            get
            {
                if (startSpeed > CurrentSpeed)
                    return 0;

                int numerator = CurrentSpeed - startSpeed;
                int denominator = TargetSpeed - startSpeed;

                if (denominator > 0)
                {
                    return ((double)numerator / denominator) * 100;
                }
                return 0;
            }
        }

    }
}
