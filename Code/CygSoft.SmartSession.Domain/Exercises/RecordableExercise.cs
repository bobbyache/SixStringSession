using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CygSoft.SmartSession.Domain.Keywords;
using CygSoft.SmartSession.Domain.Sessions;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public class RecordableExercise
    {
        internal IExercise Exercise { get; private set; }
        private IExerciseRecorder exerciseRecorder;

        public RecordableExercise(IExercise exercise)
        {
            this.Exercise = exercise ?? throw new ArgumentNullException("Exercise must be provided.");
            this.exerciseRecorder = new ExerciseRecorder();
        }
    }
}
