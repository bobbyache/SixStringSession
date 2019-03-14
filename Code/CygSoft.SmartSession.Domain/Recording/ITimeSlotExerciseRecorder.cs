using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Recording
{
    public interface  ITimeSlotExerciseRecorder : IExerciseRecorder
    {
        double AssignedSeconds { get; }
        double RemainingSeconds { get; }
    }
}
