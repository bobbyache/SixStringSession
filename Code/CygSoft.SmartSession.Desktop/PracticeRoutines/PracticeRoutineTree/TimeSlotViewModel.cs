using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Infrastructure;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.PracticeRoutines.PracticeRoutineTree
{
    public class TimeSlotViewModel : ViewModelBase
    {
        public TimeSlotViewModel(PracticeRoutineTimeSlot timeSlot)
        {
            this.timeSlot = timeSlot ?? throw new ArgumentNullException("Time Slot must be provided.");
            AddCommand = new RelayCommand(() => Add(), () => true);
            RemoveSelectionCommand = new RelayCommand(() => RemoveSelection(), () => true);

            foreach (var exercise in timeSlot)
            {
                Exercises.Add(new TimeSlotExerciseViewModel(exercise));
            }
        }

        private PracticeRoutineTimeSlot timeSlot;

        public string Title
        {
            get
            {
                return timeSlot.Title;
            }
            set
            {
                timeSlot.Title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public BindingList<TimeSlotExerciseViewModel> Exercises { get; } = new BindingList<TimeSlotExerciseViewModel>();

        public string DisplayTime => TimeFuncs.DisplayTimeFromSeconds(AssignedSeconds);

        public int AssignedSeconds
        {
            get
            {
                return timeSlot.AssignedSeconds;
            }
            set
            {
                timeSlot.AssignedSeconds = value;
                RaisePropertyChanged(() => AssignedSeconds);
                RaisePropertyChanged(() => DisplayTime);
            }
        }

        public TimeSlotExerciseViewModel SelectedExercise { get; set; }

        public RelayCommand RemoveSelectionCommand { get; private set; }
        public RelayCommand AddCommand { get; private set; }

        private void Add()
        {
            TimeSlotExercise exercise = new TimeSlotExercise(1, timeSlot.Id, "New Exercise", 3);
            Exercises.Add(new TimeSlotExerciseViewModel(exercise));
        }

        private void RemoveSelection()
        {
            Exercises.Remove(SelectedExercise);
        }
    }
}
