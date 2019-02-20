using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.RecordingRoutines
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

        public double CalculateProgress()
        {
            if (TargetTime == 0)
                return 100;

            var percentComplete = ((double)CurrentTime / TargetTime) * 100d;
            return percentComplete > 100 ? 100 : percentComplete;
        }

        public void AddMinutes(int minutes)
        {
            CurrentTime += minutes * 60;
        }

        public void AddSeconds(int seconds)
        {
            CurrentTime += seconds;
        }

        public void SubstractSeconds(int seconds)
        {
            CurrentTime -= seconds;
        }

        public void SubtractMinutes(int minutes)
        {
            CurrentTime -= minutes * 60;
        }
    }
}
