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


    public enum PercentCompleteCalculationStrategy
    {
        [System.ComponentModel.Description("Metronome Speed")]
        MetronomeSpeed = 0,
        [System.ComponentModel.Description("Practice Time")]
        PracticeTime = 1,
    }
}

