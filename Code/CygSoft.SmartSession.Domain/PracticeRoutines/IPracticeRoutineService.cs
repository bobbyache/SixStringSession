using CygSoft.SmartSession.Domain.Recording;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.PracticeRoutines
{
    public interface IPracticeRoutineService
    {
        PracticeRoutine Create();
        PracticeRoutineExercise CreatePracticeRoutineExerciseFor(int exerciseId);
        PracticeRoutine Get(int id);
        IEnumerable<PracticeRoutine> Find(PracticeRoutineSearchCriteria searchCriteria);
        void Remove(int id);
        void Add(PracticeRoutine practiceRoutine);
        void Update(PracticeRoutine practiceRoutine);
        PracticeRoutineRecorder GetPracticeRoutineRecorder(int id);
    }
}
