namespace CygSoft.SmartSession.Desktop.Exercises
{
    internal class EndEditingExerciseMessage
    {
        public int ExerciseId { get; }

        public EndEditingExerciseMessage(int exerciseId)
        {
            ExerciseId = exerciseId;
        }
    }
}

