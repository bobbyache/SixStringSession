using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Domain.Recording;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Dal.Files.Repositories
{
    public class PracticeRoutineFileRepository : IPracticeRoutineFileRepository
    {
        public void Add(PracticeRoutine entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<PracticeRoutine> entities)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<PracticeRoutine> Find(object criteria)
        {
            return new List<PracticeRoutine>
            {
                new PracticeRoutine(1, "Practice Routine 1", GetPracticeRoutineTimeSlots()),
                new PracticeRoutine(2, "Practice Routine 2", GetPracticeRoutineTimeSlots()),
            };
        }

        public PracticeRoutine Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(PracticeRoutine entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<PracticeRoutine> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(PracticeRoutine entity)
        {
            throw new NotImplementedException();
        }

        private List<PracticeRoutineTimeSlot> GetPracticeRoutineTimeSlots()
        {
            return new List<PracticeRoutineTimeSlot> {
                new PracticeRoutineTimeSlot(1, "Timeslot 1", 300,
                    new List<TimeSlotExercise>
                    {
                        new TimeSlotExercise(1, 1, "Exercise 1", 5),
                        new TimeSlotExercise(1, 1, "Exercise 2", 5)
                    })
            };
        }
    }
}
