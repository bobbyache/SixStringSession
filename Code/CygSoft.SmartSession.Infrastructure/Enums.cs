using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Infrastructure.Enums
{
    public enum EntityLifeCycleState
    {
        AsNewEntity,
        AsExistingEntity
    }

    public enum EditorCloseOperation
    {
        Saved,
        Canceled
    }

    public enum PercentCompleteCalculationStrategy
    {
        [Description("Metronome Speed")]
        MetronomeSpeed = 0,
        [Description("Practice Time")]
        PracticeTime = 1
    }
}
