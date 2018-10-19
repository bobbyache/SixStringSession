using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class OpenExerciseRecorderMessage
    {
        public int ExerciseId { get; }

        public OpenExerciseRecorderMessage(int exerciseId)
        {
            ExerciseId = exerciseId;
        }
    }
}
