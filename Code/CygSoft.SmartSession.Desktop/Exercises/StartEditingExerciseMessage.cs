using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Infrastructure.Enums;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    internal class StartEditingExerciseMessage
    {
        public IExercise Exercise { get;}

        public StartEditingExerciseMessage(IExercise exercise)
        {
            Exercise = exercise;
        }
    }
}