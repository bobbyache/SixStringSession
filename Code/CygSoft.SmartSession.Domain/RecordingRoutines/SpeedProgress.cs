using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.RecordingRoutines
{
    public class SpeedProgress : ISpeedProgress
    {
        public SpeedProgress(int initialSpeed, int currentSpeed, int targetSpeed, int weighting)
        {
            InitialSpeed = initialSpeed;
            CurrentSpeed = currentSpeed;
            TargetSpeed = targetSpeed;
        }

        public int InitialSpeed { get; }

        public int CurrentSpeed { get; private set; }

        public int TargetSpeed { get; }

        public int Weighting { get; }

        public double CalculateProgress()
        {
            if (TargetSpeed == 0)
                return 100;

            if (CurrentSpeed <= InitialSpeed)
                return 0;

            var numerator = (double)(CurrentSpeed - InitialSpeed);
            var denominator = (double)(TargetSpeed - InitialSpeed);

            var percentComplete = (numerator / denominator) * 100d;
            return percentComplete > 100 ? 100 : percentComplete;
        }

        public void AddTicks(int ticks)
        {
            CurrentSpeed += ticks;
        }

        public void SubtractTicks(int ticks)
        {
            CurrentSpeed -= ticks;
        }
    }
}
