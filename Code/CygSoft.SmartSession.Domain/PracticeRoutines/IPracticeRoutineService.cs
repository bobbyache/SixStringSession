using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.PracticeRoutines
{
    public interface IPracticeRoutineService
    {
        PracticeRoutine Create();
        PracticeRoutine Get(int id);
        IEnumerable<PracticeRoutine> Find(PracticeRoutineSearchCriteria searchCriteria);
        void Remove(int id);
        void Add(PracticeRoutine practiceRoutine);
        void Update(PracticeRoutine practiceRoutine);
    }
}
