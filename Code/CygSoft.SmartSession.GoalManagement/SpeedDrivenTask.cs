using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement
{
    public class SpeedDrivenTask : GoalTask
    {
        private int currentSpeed;
        private int speedIncrement;
        private int targetSpeed;

        public SpeedDrivenTask(int weighting, int minutesRecorded, int currentSpeed, int targetSpeed, int speedIncrement)
            : base(weighting, minutesRecorded)
        {
            this.currentSpeed = currentSpeed;
            this.targetSpeed = targetSpeed;
            this.speedIncrement = speedIncrement;
        }

        public int CurrentSpeed => currentSpeed;

        public int SpeedIncrement => speedIncrement;

        public int TargetSpeed => targetSpeed;

        public override int PercentCompleted
        {
            get
            {
                // The idea is that if one starts off with a metronome speed of 50, but your 
                // target metronome speed is 90, and your currently checked off speed is 70, then your percentage will be...
                // ((90 - 70) / (90 - 50)) * 100 ...
                throw new NotImplementedException();
            }
        }

    }
}
