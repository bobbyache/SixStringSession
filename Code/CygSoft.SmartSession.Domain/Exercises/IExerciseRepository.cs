using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public interface IExerciseRepository
    {
        Exercise Get(int id);

        IEnumerable<Exercise> Find(string titleFragment);

        void Delete(int id);

        //void Update(Exercise exercise);

        void Add(Exercise exercise);

        void SaveChanges();
    }
}
