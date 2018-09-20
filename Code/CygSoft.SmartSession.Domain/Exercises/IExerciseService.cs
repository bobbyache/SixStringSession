using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public interface IExerciseService
    {
        Exercise Get(int id);
        IEnumerable<Exercise> Find(ExerciseSearchCriteria searchCriteria);
        void Remove(int id);
        void Add(Exercise exercise);
        void Update(Exercise exercise);
    }
}
