using CygSoft.SmartSession.Domain.Common;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public interface IExerciseRepository : IRepository<Exercise>
    {
        // Only add really custom stuff here like...
        // GetExercisesWhere...
        IReadOnlyList<Exercise> Find(Specification<Exercise> specification, string[] keywords, int page = 0, int pageSize = 100);
    }
}
