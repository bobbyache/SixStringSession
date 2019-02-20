using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.RecordingRoutines
{
    public interface ISpeedProgress
    {
        int InitialSpeed { get; }
        int CurrentSpeed { get; }

        int TargetSpeed { get; }

        int Weighting { get; }
        double CalculateProgress();

        ISpeedProgress AddTicks(int ticks);
        ISpeedProgress SubtractTicks(int ticks);
    }
}
