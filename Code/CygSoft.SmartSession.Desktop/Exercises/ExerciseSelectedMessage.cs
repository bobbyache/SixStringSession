using CygSoft.SmartSession.Domain.Exercises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    internal class ExerciseSelectedMessage
    {
        public int ExerciseId { get; }

        public ExerciseSelectedMessage(int exerciseId)
        {
            ExerciseId = exerciseId;
        }
    }
}
