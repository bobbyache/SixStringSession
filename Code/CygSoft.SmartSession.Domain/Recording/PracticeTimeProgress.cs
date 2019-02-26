using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Recording
{
    public class PracticeTimeProgress : IPracticeTimeProgress
    {
        public PracticeTimeProgress(int currentTime, int targetTime, int weighting)
        {
            CurrentTime = currentTime;
            TargetTime = targetTime;
            Weighting = weighting;
        }

        public int CurrentTime { get; private set; }

        public int TargetTime { get; }

        public int Weighting { get; }

        public int CalculateProgress()
        {
            if (TargetTime == 0)
                return 100;

            var percentComplete = ((double)CurrentTime / TargetTime) * 100d;
            return (int)Math.Round(percentComplete > 100 ? 100 : percentComplete, 0);
        }

        public IPracticeTimeProgress AddMinutes(int minutes)
        {
            var addedTime = CurrentTime + (minutes * 60);
            var progress = new PracticeTimeProgress(addedTime, this.TargetTime, this.Weighting);

            return progress;
        }

        public IPracticeTimeProgress AddSeconds(int seconds)
        {
            var addedTime = CurrentTime + seconds;
            var progress = new PracticeTimeProgress(addedTime, this.TargetTime, this.Weighting);

            return progress;
        }

        public IPracticeTimeProgress SubstractSeconds(int seconds)
        {
            var time = CurrentTime - seconds;
            if (time < 0) time = 0;

            var progress = new PracticeTimeProgress(time, this.TargetTime, this.Weighting);

            return progress;
        }

        public IPracticeTimeProgress SubtractMinutes(int minutes)
        {
            var time = CurrentTime - (minutes * 60);
            if (time < 0) time = 0;

            var progress = new PracticeTimeProgress(time, this.TargetTime, this.Weighting);

            return progress;
        }
    }
}
