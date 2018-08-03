using CygSoft.SmartSession.Domain.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.Domain.Tasks
{
    public class MetronomeGoalTask : GoalTask
    {
        //public MetronomeGoalTask(string title, DateTime createDate, int startSpeed, int targetSpeed, List<MetronomeSessionResult> results) 
        //    : base(title, createDate, results.OfType<SessionResult>().ToList())
        //{
        //    if (startSpeed > targetSpeed)
        //        throw new ArgumentOutOfRangeException("Start metronome speed cannot exceed the target metronome speed.");

        //    this.targetSpeed = targetSpeed;
        //    this.startSpeed = startSpeed;
        //}


        public int TargetSpeed { get; set; }
        public int StartSpeed { get; set; }

        public int CurrentSpeed
        {
            get
            {
                if (sessionResults == null)
                    return 0;

                if (sessionResults.Count == 0)
                    return 0;

                return sessionResults.OfType<MetronomeSessionResult>().OrderBy(r => r.StartTime).Last().Speed;
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
