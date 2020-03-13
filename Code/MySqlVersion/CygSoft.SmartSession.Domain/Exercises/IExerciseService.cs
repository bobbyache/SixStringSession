using CygSoft.SmartSession.Domain.Recording;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public interface IExerciseService
    {
        IExercise Create();
        ExerciseActivity CreateExerciseActivity(int speed, int seconds, int manualProgress);

        IExercise Get(int id);
        IEnumerable<IExercise> Find(ExerciseSearchCriteria searchCriteria);
        IEnumerable<IExercise> GetPracticeRoutineExercises(int practiceRoutineId);

        IExerciseRecorder GetExerciseRecorder(int exerciseId);

        void Remove(int id);
        void Add(IExercise exercise);
        void Update(IExercise exercise);
        void Update(IEnumerable<IExercise> exercises);
    }
}
