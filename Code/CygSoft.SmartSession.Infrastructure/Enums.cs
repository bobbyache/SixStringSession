using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Infrastructure.Enums
{
    public enum PercentCompleteCalculationStrategy
    {
        [Description("Metronome Speed")]
        MetronomeSpeed = 0,
        [Description("Practice Time")]
        PracticeTime = 1,
        [Description("Manual Percentage")]
        BasicPercent = 2
    }
}
