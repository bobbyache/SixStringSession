using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.RecordingRoutines
{
    public interface IPracticeTimeProgress
    {
        int CurrentTime { get; }
        int TargetTime { get; }

        int Weighting { get; }
        double CalculateProgress();

        void AddSeconds(int seconds);
        void SubstractSeconds(int seconds);
        void AddMinutes(int minutes);
        void SubtractMinutes(int minutes);
    }
}
