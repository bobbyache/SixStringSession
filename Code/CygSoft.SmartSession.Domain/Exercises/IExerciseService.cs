using CygSoft.SmartSession.Domain.Sessions;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public interface IExerciseService
    {
        IExercise Create();
        ExerciseActivity CreateExerciseActivity(int speed, int seconds, int manualProgress, DateTime startTime, DateTime endTime);

        IExercise Get(int id);
        IEnumerable<IExercise> Find(ExerciseSearchCriteria searchCriteria);
        IEnumerable<IExercise> GetPracticeRoutineExercises(int practiceRoutineId);
        void Remove(int id);
        void Add(IExercise exercise);
        void Update(IExercise exercise);
        void Update(IEnumerable<IExercise> exercises);

        void AddFiles(int exerciseId, string[] filePaths);
        void DeleteFiles(int exerciseId);
        void DeleteFiles(int exerciseId, string[] fileNames);
        string[] GetFiles(int exerciseId);
        void OpenFile(int exerciseId, string fileName);
    }
}
