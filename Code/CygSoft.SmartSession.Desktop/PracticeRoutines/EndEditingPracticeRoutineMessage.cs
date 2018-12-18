using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.PracticeRoutines
{
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
}
