using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.PracticeRoutines
{
    public class PracticeRoutine : Entity
    {
        // --------------------------------------------------------------------------------------
        // TODO: Remove this when you are able...
        // --------------------------------------------------------------------------------------
        public List<PracticeRoutineExercise> PracticeRoutineExercises { get; set; } = new List<PracticeRoutineExercise>();
        // --------------------------------------------------------------------------------------

        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Title { get; set; }

        public IEnumerable<PracticeRoutineTimeSlot> TimeSlots { get; private set; }

        public PracticeRoutine(string title, IEnumerable<PracticeRoutineTimeSlot> timeSlots)
        {
            Title = title;
            TimeSlots = timeSlots ?? throw new ArgumentNullException("Time slots must be specified.");
        }

        public PracticeRoutine(int id, string title, List<PracticeRoutineTimeSlot> timeSlots)
        {
            Title = title;
            Id = id;
            TimeSlots = timeSlots ?? throw new ArgumentNullException("Time slots must be specified.");
        }
    }

    public class PracticeRoutineTimeSlot : Entity
    {
        public PracticeRoutineTimeSlot(IEnumerable<Exercise> exercises)
        {
            Exercises = exercises ?? throw new ArgumentNullException("Exercises must be specified.");
        }

        public IEnumerable<Exercise> Exercises { get; private set; }
    }
}
