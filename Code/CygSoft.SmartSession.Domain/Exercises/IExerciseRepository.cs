using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public interface IExerciseRepository
    {
        Exercise Get(int id);
        IEnumerable<Exercise> Find(string titleFragment);
        void Remove(int id);
        void Add(Exercise exercise);
        void Update(Exercise exercise);
        void SaveChanges();
    }
}
