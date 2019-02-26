using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.PracticeRoutines
{
    public class PracticeRoutineExercise
    {
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public int ExerciseId { get; set; }

        public string Title { get; set; }
        public int PracticeRoutineId { get; set; }
        
        public int AssignedPracticeTime { get; set; } // in seconds

        public int Seconds { get { return AssignedPracticeTime; } set { AssignedPracticeTime = value; } }

        public int Minutes
        {
            get
            {
                if (AssignedPracticeTime == 0)
                    return 0;

                return (int)Math.Ceiling((double)AssignedPracticeTime / 60);
            }
            set { AssignedPracticeTime = value * 60; }
        }

        // 1 - 5
        public int FrequencyWeighting { get; set; }
    }
}
