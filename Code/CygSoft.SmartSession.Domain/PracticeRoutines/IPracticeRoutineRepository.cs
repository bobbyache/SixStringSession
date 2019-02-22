using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.RecordingRoutines;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.PracticeRoutines
{
    public interface IPracticeRoutineRepository : IRepository<PracticeRoutine>
    {
        //RecordingRoutine GetRecordableRoutine()
    }
}