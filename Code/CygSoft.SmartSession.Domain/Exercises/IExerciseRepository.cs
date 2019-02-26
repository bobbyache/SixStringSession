using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Recording;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public interface IExerciseRepository : IRepository<IExercise>
    {
        // Only add really custom stuff here...
        IReadOnlyList<IExercise> GetPracticeRoutineExercises(int practiceRoutineId);

        void Update(IEnumerable<IExercise> exercises);

        IExerciseRecorder GetExerciseRecorder(int id);
    }
}
