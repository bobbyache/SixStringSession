using CygSoft.SmartSession.Domain.Common;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public interface IExerciseRepository
    {
        Exercise Get(int id);
        IReadOnlyList<Exercise> Find(Specification<Exercise> specification, int page = 0, int pageSize = 100);

        void Remove(int id);
        void Add(Exercise exercise);
        void Update(Exercise exercise);
        void SaveChanges();
    }
}
