using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement
{
    public class MetronomeGoalTask : GoalTask
    {
        private DateTime dateTime;
        private int targetSpeed;
        private int startSpeed;
        private List<MetronomeSessionResult> results;

        public MetronomeGoalTask(string title) : base(title)
        {
        }

        public MetronomeGoalTask(string title, DateTime dateTime, int startSpeed, int targetSpeed, List<MetronomeSessionResult> results) : this(title)
        {
            this.dateTime = dateTime;
            this.targetSpeed = targetSpeed;
            this.startSpeed = startSpeed;
            this.results = results;
        }

        public int CurrentSpeed
        {
            get
            {
                if (results == null)
                    return 0;

                if (results.Count == 0)
                    return 0;

                return results.OrderBy(r => r.DateCreated).Last().Speed;
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
