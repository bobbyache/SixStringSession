namespace CygSoft.SmartSession.Desktop.Exercises
{
    internal class EndEditingExerciseMessage
    {
        public ExerciseModel ExerciseModel { get; }

        public EndEditingExerciseMessage(ExerciseModel exerciseModel)
        {
            ExerciseModel = exerciseModel;
        }
    }
}