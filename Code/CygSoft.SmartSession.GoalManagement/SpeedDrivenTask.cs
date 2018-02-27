using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement
{
    public class SpeedDrivenTask
    {
        private int currentSpeed;
        private int minutesRecorded;
        private int speedIncrement;
        private int targetSpeed;
        private int weighting;

        public SpeedDrivenTask(int minutesRecorded, int currentSpeed, int targetSpeed, int speedIncrement, int weighting)
        {
            this.minutesRecorded = minutesRecorded;
            this.currentSpeed = currentSpeed;
            this.targetSpeed = targetSpeed;
            this.speedIncrement = speedIncrement;
            this.weighting = weighting;
        }

        public int CurrentSpeed => currentSpeed;

        public int MinutesRecorded => minutesRecorded;

        public int SpeedIncrement => speedIncrement;

        public int TargetSpeed => targetSpeed;

        public int Weighting => weighting;
    }
}
