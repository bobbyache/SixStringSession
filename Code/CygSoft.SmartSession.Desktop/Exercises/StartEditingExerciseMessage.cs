using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Infrastructure.Enums;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    internal class StartEditingExerciseMessage
    {
        public Exercise Exercise { get;}

        public StartEditingExerciseMessage(Exercise exercise)
        {
            Exercise = exercise;
        }
    }
}