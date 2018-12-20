using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.PracticeRoutines
{
    internal class StartPracticingRoutineMessage
    {
        public int ExercisePracticeRoutineId { get; private set; }

        public StartPracticingRoutineMessage(int id)
        {
            ExercisePracticeRoutineId = id;
        }
    }
}
