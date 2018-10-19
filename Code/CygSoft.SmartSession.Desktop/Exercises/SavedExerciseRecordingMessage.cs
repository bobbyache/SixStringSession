using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class SavedExerciseRecordingMessage
    {
        public int ExerciseId { get; }

        public SavedExerciseRecordingMessage(int exerciseId)
        {
            ExerciseId = exerciseId;
        }
    }
}
