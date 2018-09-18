using CygSoft.SmartSession.Domain.Common;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public interface IExerciseRepository : IRepository<Exercise>
    {
        // Only add really custom stuff here like...
        // GetExercisesWhere...
    }
}
