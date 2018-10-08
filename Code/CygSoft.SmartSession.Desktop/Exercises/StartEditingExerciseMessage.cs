namespace CygSoft.SmartSession.Desktop.Exercises
{
    internal class StartEditingExerciseMessage
    {
        public ExerciseSearchResultModel ExerciseSearchResult { get;}

        public StartEditingExerciseMessage(ExerciseSearchResultModel exerciseSearchResult)
        {
            ExerciseSearchResult = exerciseSearchResult;
        }
    }
}