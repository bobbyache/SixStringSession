using CygSoft.SmartSession.Domain.PracticeRoutines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.PracticeRoutines
{
    internal class StartEditingPracticeRoutineMessage
    {
        public PracticeRoutine PracticeRoutine { get; }

        public StartEditingPracticeRoutineMessage(PracticeRoutine practiceRoutine)
        {
            PracticeRoutine = practiceRoutine;
        }
    }
}
