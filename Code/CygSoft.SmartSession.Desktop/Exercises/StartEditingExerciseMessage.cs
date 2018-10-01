namespace CygSoft.SmartSession.Desktop.Exercises
{
    internal class StartEditingExerciseMessage
    {
        public ExerciseSearchResult ExerciseSearchResult { get;}

        public StartEditingExerciseMessage(ExerciseSearchResult exerciseSearchResult)
        {
            ExerciseSearchResult = exerciseSearchResult;
        }
    }
}