using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Goals;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using System;

namespace CygSoft.SmartSession.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IExerciseRepository Exercises { get; }
        IGoalRepository Goals { get; }
        IPracticeRoutineRepository PracticeRoutines { get; }

        int Commit();
        void Rollback();
    }
}
