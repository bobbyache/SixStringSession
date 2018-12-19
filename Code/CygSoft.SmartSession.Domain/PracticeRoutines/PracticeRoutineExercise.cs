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

        public string Title { get; set; } = "Dummy Exercise Title";
        public int PracticeRoutineId { get; set; }

        // in seconds
        public int AssignedPracticeTime { get; set; }

        // 1 - 5
        public int DifficultyRating { get; set; }
        public int PracticalityRating { get; set; }
    }
}
