using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Infrastructure.Enums;

namespace CygSoft.SmartSession.Desktop.Supports.Messages
{
    internal class CancelledExerciseRecordingMessage
    {
    }

    internal class EndEditingExerciseMessage
    {
        public IExercise Exercise { get; }
        public EditorCloseOperation Operation { get; }

        public EntityLifeCycleState LifeCycleState { get; }

        public EndEditingExerciseMessage(IExercise exercise, EditorCloseOperation operation, EntityLifeCycleState lifeCycleState)
        {
            Exercise = exercise;
            Operation = operation;
            LifeCycleState = lifeCycleState;
        }
    }

    internal class ExerciseSelectedMessage
    {
        public int ExerciseId { get; }

        public ExerciseSelectedMessage(int exerciseId)
        {
            ExerciseId = exerciseId;
        }
    }

    internal class ExerciseSelectionCancelledMessage
    {
    }

    internal class OpenExerciseRecorderMessage
    {
        public int ExerciseId { get; }

        public OpenExerciseRecorderMessage(int exerciseId)
        {
            ExerciseId = exerciseId;
        }
    }

    internal class SavedExerciseRecordingMessage
    {
        public int ExerciseId { get; }

        public SavedExerciseRecordingMessage(int exerciseId)
        {
            ExerciseId = exerciseId;
        }
    }

    internal class StartEditingExerciseMessage
    {
        public IExercise Exercise { get; }

        public StartEditingExerciseMessage(IExercise exercise)
        {
            Exercise = exercise;
        }
    }

    internal class FindGoalsMessage
    {
    }

    internal class EndEditingPracticeRoutineMessage
    {
        public PracticeRoutine PracticeRoutine { get; }
        public EditorCloseOperation Operation { get; }

        public EntityLifeCycleState LifeCycleState { get; }

        public EndEditingPracticeRoutineMessage(PracticeRoutine practiceRoutine, EditorCloseOperation operation,
            EntityLifeCycleState lifeCycleState)
        {
            PracticeRoutine = practiceRoutine;
            Operation = operation;
            LifeCycleState = lifeCycleState;
        }
    }

    internal class ExitPracticeListMessage
    {
    }

    internal class StartEditingPracticeRoutineMessage
    {
        public PracticeRoutine PracticeRoutine { get; }

        public StartEditingPracticeRoutineMessage(PracticeRoutine practiceRoutine)
        {
            PracticeRoutine = practiceRoutine;
        }
    }

    public class StartSelectingPracticeRoutineExerciseMessage
    {
    }

    public class ViewPracticeListMessage
    {
        public int PracticeRoutineId { get; private set; }
        public ViewPracticeListMessage(int practiceRoutineId)
        {
            PracticeRoutineId = practiceRoutineId;
        }
    }
}
