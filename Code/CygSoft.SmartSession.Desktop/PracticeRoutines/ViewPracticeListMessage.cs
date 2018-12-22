using CygSoft.SmartSession.Domain.PracticeRoutines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.PracticeRoutines
{
    public class ViewPracticeListMessage
    {
        public int PracticeRoutineId { get; private set; }
        public ViewPracticeListMessage(int practiceRoutineId)
        {
            PracticeRoutineId = practiceRoutineId;
        }
    }
}
