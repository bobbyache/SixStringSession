using CygSoft.SmartSession.Domain.Sessions;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public interface IExerciseService
    {
        Exercise Create();
        ExerciseActivity CreateExerciseActivity(int speed, int seconds, int manualProgress, DateTime startTime, DateTime endTime);

        Exercise Get(int id);
        IEnumerable<Exercise> Find(ExerciseSearchCriteria searchCriteria);
        IEnumerable<Exercise> GetPracticeRoutineExercises(int practiceRoutineId);
        void Remove(int id);
        void Add(Exercise exercise);
        void Update(Exercise exercise);
        void Update(IEnumerable<Exercise> exercises);

        void AddFiles(int exerciseId, string[] filePaths);
        void DeleteFiles(int exerciseId);
        void DeleteFiles(int exerciseId, string[] fileNames);
        string[] GetFiles(int exerciseId);
        void OpenFile(int exerciseId, string fileName);
    }
}
