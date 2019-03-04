using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.PracticeRoutines
{
    public class PracticeRoutineTimeSlot : Entity, IList<TimeSlotExercise>
    {
        private List<TimeSlotExercise> exercises;
        public string Title { get; set; }
        public int AssignedSeconds { get; set; }

        public int Count => exercises.Count;

        public bool IsReadOnly => false;

        public TimeSlotExercise this[int index]
        {
            get => exercises[index];
            set => exercises[index] = value;
        }

        public PracticeRoutineTimeSlot(int id, string title, int assignedSeconds, List<TimeSlotExercise> exercises)
        {
            Id = id;
            Title = title;
            AssignedSeconds = assignedSeconds;
            this.exercises = exercises ?? throw new ArgumentNullException("Exercises must be specified.");
        }

        public PracticeRoutineTimeSlot(string title, int assignedSeconds, List<TimeSlotExercise> exercises)
        {
            Title = title;
            AssignedSeconds = assignedSeconds;
            this.exercises = exercises ?? throw new ArgumentNullException("Exercises must be specified.");
        }

        public int IndexOf(TimeSlotExercise item)
        {
            return exercises.IndexOf(item);
        }

        public void Insert(int index, TimeSlotExercise item)
        {
            exercises.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            exercises.RemoveAt(index);
        }

        public void Add(TimeSlotExercise item)
        {
            exercises.Add(item);
        }

        public void Clear()
        {
            exercises.Clear();
        }

        public bool Contains(TimeSlotExercise item)
        {
            return exercises.Contains(item);
        }

        public void CopyTo(TimeSlotExercise[] array, int arrayIndex)
        {
            exercises.CopyTo(array, arrayIndex);
        }

        public bool Remove(TimeSlotExercise item)
        {
            return exercises.Remove(item);
        }

        public IEnumerator<TimeSlotExercise> GetEnumerator()
        {
            return exercises.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return exercises.GetEnumerator();
        }
    }
}
