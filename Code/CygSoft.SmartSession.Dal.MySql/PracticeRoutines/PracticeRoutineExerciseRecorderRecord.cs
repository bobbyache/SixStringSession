using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Dal.MySql.PracticeRoutines
{
    internal class PracticeRoutineExerciseRecorderRecord
    {
        public int PracticeRoutineId { get; set; }
        public int ExerciseId { get; set; }

        public string TimeSlotTitle { get; set; }
        public string ExerciseTitle { get; set; }

        public int InitialRecordedSpeed { get; set; }

        public int LastRecordedSpeed { get; set; }

        public int LastRecordedManualProgress { get; set; }

        public int TotalPracticeTime { get; set; }

        public int AssignedPracticeTime { get; set; }

        public int TargetMetronomeSpeed { get; set; }

        public int TargetPracticeTime { get; set; }

        public int ManualProgressWeighting { get; set; }

        public int SpeedProgressWeighting { get; set; }

        public int PracticeTimeProgressWeighting { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
