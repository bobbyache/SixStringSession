using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Infrastructure.Enums;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    internal class EndEditingExerciseMessage
    {
        public Exercise Exercise { get; }
        public EditorCloseOperation Operation { get; }

        public EntityLifeCycleState LifeCycleState { get; }

        public EndEditingExerciseMessage(Exercise exercise, EditorCloseOperation operation, EntityLifeCycleState lifeCycleState)
        {
            Exercise = exercise;
            Operation = operation;
            LifeCycleState = lifeCycleState;
        }
    }
}

