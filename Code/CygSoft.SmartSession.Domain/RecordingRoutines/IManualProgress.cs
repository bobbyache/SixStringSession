using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.RecordingRoutines
{
    public interface IManualProgress
    {
        double Weighting { get; }
        double CalculateProgress();

        void Increase(int value);
        void Decrease(int value);
    }
}
