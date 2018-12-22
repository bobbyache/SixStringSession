using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Sessions;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public interface IExerciseRepository : IRepository<Exercise>
    {
        // Only add really custom stuff here...
        IReadOnlyList<Exercise> GetPracticeRoutineExercises(int practiceRoutineId);

        void Update(IEnumerable<Exercise> exercises);
    }
}
