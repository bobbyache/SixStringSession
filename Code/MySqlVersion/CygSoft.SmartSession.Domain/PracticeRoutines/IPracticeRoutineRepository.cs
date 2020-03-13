using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Recording;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.PracticeRoutines
{
    public interface IPracticeRoutineRepository : IRepository<PracticeRoutine>
    {
        PracticeRoutineRecorder GetPracticeRoutineRecorder(int id);
        IReadOnlyList<PracticeRoutineHeader> Find(IPracticeRoutineSearchCriteria criteria);
    }
}