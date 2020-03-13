using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using System;

namespace CygSoft.SmartSession.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IExerciseRepository Exercises { get; }
        IPracticeRoutineRepository PracticeRoutines { get; }

        int Commit();
        void Rollback();
    }
}
