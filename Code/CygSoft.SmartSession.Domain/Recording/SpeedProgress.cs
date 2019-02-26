using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Recording
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

        public int CalculateProgress()
        {
            if (TargetSpeed == 0)
                return 100;

            if (CurrentSpeed <= InitialSpeed)
                return 0;

            var numerator = (double)(CurrentSpeed - InitialSpeed);
            var denominator = (double)(TargetSpeed - InitialSpeed);

            var percentComplete = (numerator / denominator) * 100d;
            return (int)Math.Round(percentComplete > 100 ? 100 : percentComplete, 0);
        }

        public ISpeedProgress AddTicks(int ticks)
        {
            var currentTicks = CurrentSpeed + ticks;
            var progress = new SpeedProgress(this.InitialSpeed, currentTicks, this.TargetSpeed, this.Weighting);

            return progress;
        }

        public ISpeedProgress NewSpeedSpeedProgress(int value)
        {
            var progress = new SpeedProgress(this.InitialSpeed, value, this.TargetSpeed, this.Weighting);

            return progress;
        }

        public ISpeedProgress SubtractTicks(int ticks)
        {
            var currentTicks = CurrentSpeed - ticks;
            if (currentTicks < 0) currentTicks = 0;

            var progress = new SpeedProgress(this.InitialSpeed, currentTicks, this.TargetSpeed, this.Weighting);

            return progress;
        }
    }
}
