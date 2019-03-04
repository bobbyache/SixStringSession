using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CygSoft.SmartSession.Domain.PracticeRoutines
{
    public class PracticeRoutine : Entity, IList<PracticeRoutineTimeSlot>
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Title { get; set; }

        protected List<PracticeRoutineTimeSlot> timeSlots { get; } = new List<PracticeRoutineTimeSlot>();

        public int Count => timeSlots.Count;

        public bool IsReadOnly => false;

        public PracticeRoutineTimeSlot this[int index]
        {
            get => timeSlots[index];
            set => timeSlots[index] = value;
        }

        public PracticeRoutine(string title, IEnumerable<PracticeRoutineTimeSlot> timeSlots)
        {
            Title = title;

            if (timeSlots == null)
                throw new ArgumentNullException("Time slots must be specified.");

            //TODO: Create Test - Ensure no timeslots get created with duplicate names.
            foreach (var timeSlot in timeSlots)
                this.timeSlots.Add(timeSlot);
        }

        public PracticeRoutine(int id, string title, List<PracticeRoutineTimeSlot> timeSlots)
        {
            Title = title;
            Id = id;
            this.timeSlots = timeSlots ?? throw new ArgumentNullException("Time slots must be specified.");
        }

        public int IndexOf(PracticeRoutineTimeSlot item)
        {
            return timeSlots.IndexOf(item);
        }

        public void Insert(int index, PracticeRoutineTimeSlot item)
        {
            timeSlots.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            timeSlots.RemoveAt(index);
        }

        public void Add(PracticeRoutineTimeSlot item)
        {
            if (item.Id > 0)
                throw new ArgumentException("Only newly created time slots can be added to a practice routine. PracticeRoutineTimeSlot has an Id greater than 0.");

            var exists = timeSlots.Where(tslot => tslot.Title == item.Title).Any();
            if (exists)
                throw new ArgumentException("Cannot add a duplicate time slot to a routine.");

            timeSlots.Add(item);
        }

        public void Clear()
        {
            timeSlots.Clear();
        }

        public bool Contains(PracticeRoutineTimeSlot item)
        {
            return timeSlots.Contains(item);
        }

        public void CopyTo(PracticeRoutineTimeSlot[] array, int arrayIndex)
        {
            timeSlots.CopyTo(array, arrayIndex);
        }

        public bool Remove(PracticeRoutineTimeSlot item)
        {
            return timeSlots.Remove(item);
        }

        public IEnumerator<PracticeRoutineTimeSlot> GetEnumerator()
        {
            return timeSlots.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return timeSlots.GetEnumerator();
        }
    }
}
