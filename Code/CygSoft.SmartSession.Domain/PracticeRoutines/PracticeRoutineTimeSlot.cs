using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.PracticeRoutines
{
    public class PracticeRoutineTimeSlot : Entity
    {
        public List<TimeSlotExercise> Exercises { get; private set; }
        public string Title { get; set; }
        public int AssignedSeconds { get; set; }

        public PracticeRoutineTimeSlot(int id, string title, int assignedSeconds, List<TimeSlotExercise> exercises)
        {
            Id = id;
            Title = title;
            AssignedSeconds = assignedSeconds;
            Exercises = exercises ?? throw new ArgumentNullException("Exercises must be specified.");
        }

        public PracticeRoutineTimeSlot(string title, int assignedSeconds, List<TimeSlotExercise> exercises)
        {
            Title = title;
            AssignedSeconds = assignedSeconds;
            Exercises = exercises ?? throw new ArgumentNullException("Exercises must be specified.");
        }
    }
}
