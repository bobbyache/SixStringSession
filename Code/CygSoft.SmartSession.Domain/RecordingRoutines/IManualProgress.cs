using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.RecordingRoutines
{
    public interface IManualProgress
    {
        int Weighting { get; }
        double CalculateProgress();

        IManualProgress Increase(int value);
        IManualProgress Decrease(int value);
    }
}
