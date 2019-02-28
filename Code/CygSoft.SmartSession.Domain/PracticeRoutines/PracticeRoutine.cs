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
        public int TimeSlotCount { get { return TimeSlots.Count;  } }

        public List<PracticeRoutineTimeSlot> TimeSlots { get; } = new List<PracticeRoutineTimeSlot>();

        public PracticeRoutine(string title, IEnumerable<PracticeRoutineTimeSlot> timeSlots)
        {
            Title = title;

            if (timeSlots == null)
                throw new ArgumentNullException("Time slots must be specified.");

            //TODO: Create Test - Ensure no timeslots get created with duplicate names.
            foreach (var timeSlot in timeSlots)
                TimeSlots.Add(timeSlot);
        }

        public PracticeRoutine(int id, string title, List<PracticeRoutineTimeSlot> timeSlots)
        {
            Title = title;
            Id = id;
            TimeSlots = timeSlots ?? throw new ArgumentNullException("Time slots must be specified.");
        }

        public void AddTimeSlot(PracticeRoutineTimeSlot timeSlot)
        {
            if (timeSlot.Id > 0)
                throw new ArgumentException("Only newly created time slots can be added to a practice routine. PracticeRoutineTimeSlot has an Id greater than 0.");

            var exists = TimeSlots.Where(tslot => tslot.Title == timeSlot.Title).Any();
            if (exists)
                throw new ArgumentException("Cannot add a duplicate time slot to a routine.");

            TimeSlots.Add(timeSlot);
        }

        public void RemoveTimeSlot(PracticeRoutineTimeSlot timeSlot)
        {
            TimeSlots.Remove(timeSlot);
        }
    }
}
