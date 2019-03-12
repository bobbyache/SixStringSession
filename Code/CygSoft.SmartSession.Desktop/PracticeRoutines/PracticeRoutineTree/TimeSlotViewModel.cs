using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Infrastructure;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;

namespace CygSoft.SmartSession.Desktop.PracticeRoutines.PracticeRoutineTree
{
    public class TimeSlotViewModel : ViewModelBase
    {
        public TimeSlotViewModel(PracticeRoutineTimeSlot timeSlot)
        {
            this.TimeSlot = timeSlot ?? throw new ArgumentNullException("Time Slot must be provided.");
            IncrementMinutesCommand = new RelayCommand(() => IncrementMinutesPracticed(), () => true);
            DecrementMinutesCommand = new RelayCommand(() => DecrementMinutesPracticed(), () => true);

            foreach (var exercise in timeSlot)
                Exercises.Add(new TimeSlotExerciseViewModel(exercise, this));
        }

        public PracticeRoutineTimeSlot TimeSlot { get; }

        public string Title
        {
            get
            {
                return TimeSlot.Title;
            }
            set
            {
                TimeSlot.Title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public BindingList<TimeSlotExerciseViewModel> Exercises { get; } = new BindingList<TimeSlotExerciseViewModel>();

        public RelayCommand IncrementMinutesCommand { get; private set; }
        public RelayCommand DecrementMinutesCommand { get; private set; }

        public string DisplayTime => TimeFuncs.DisplayTimeFromSeconds(AssignedSeconds);

        public int AssignedSeconds
        {
            get
            {
                return TimeSlot.AssignedSeconds;
            }
            set
            {
                TimeSlot.AssignedSeconds = value;
                RaisePropertyChanged(() => AssignedSeconds);
                RaisePropertyChanged(() => DisplayTime);
            }
        }

        public TimeSlotExerciseViewModel SelectedExercise { get; set; }

        private void RemoveSelection()
        {
            Exercises.Remove(SelectedExercise);
        }

        public void Remove(TimeSlotExerciseViewModel timeSlotExerciseViewModel)
        {
            Exercises.Remove(timeSlotExerciseViewModel);
        }

        private void DecrementMinutesPracticed()
        {
            AssignedSeconds -= 60;
        }

        private void IncrementMinutesPracticed()
        {
            AssignedSeconds += 60;
        }
    }
}
