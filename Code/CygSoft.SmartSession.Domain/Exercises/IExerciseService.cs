using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public interface IExerciseService
    {
        Exercise Create();
        Exercise Get(int id);
        IEnumerable<Exercise> Find(ExerciseSearchCriteria searchCriteria);
        void Remove(int id);
        void Add(Exercise exercise);
        void Update(Exercise exercise);

        void AddFiles(int exerciseId, string[] filePaths);
        void DeleteFiles(int exerciseId);
        void DeleteFiles(int exerciseId, string[] fileNames);
        string[] GetFiles(int exerciseId);
        void OpenFile(int exerciseId, string fileName);
    }
}
