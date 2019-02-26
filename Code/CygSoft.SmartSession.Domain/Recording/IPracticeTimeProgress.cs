using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Recording
{
    public interface IPracticeTimeProgress
    {
        int CurrentTime { get; }
        int TargetTime { get; }

        int Weighting { get; }
        int CalculateProgress();

        IPracticeTimeProgress AddSeconds(int seconds);
        IPracticeTimeProgress SubstractSeconds(int seconds);
        IPracticeTimeProgress AddMinutes(int minutes);
        IPracticeTimeProgress SubtractMinutes(int minutes);
    }
}
