namespace CygSoft.SmartSession.Desktop.Exercises
{
    internal class EditExerciseMessage
    {
        public ExerciseSearchResult Exercise { get; private set; }

        public EditExerciseMessage(ExerciseSearchResult exerciseSearchResult)
        {
            Exercise = exerciseSearchResult;
        }
    }
}