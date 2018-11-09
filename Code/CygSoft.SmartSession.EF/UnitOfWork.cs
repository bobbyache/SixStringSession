using CygSoft.SmartSession.Domain;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Goals;
using CygSoft.SmartSession.EF.Repositories;

namespace CygSoft.SmartSession.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SmartSessionContext context;

        public IGoalRepository Goals { get; private set; }
        public IExerciseRepository Exercises { get; private set; }

        public UnitOfWork(SmartSessionContext context)
        {
            this.context = context;
            Goals = new GoalRepository(context);
            Exercises = new ExerciseRepository(context);
        }

        public int Commit()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void Rollback()
        {
            throw new System.NotImplementedException();
        }
    }
}
