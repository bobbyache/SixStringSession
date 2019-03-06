using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Infrastructure;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CygSoft.SmartSession.Desktop.PracticeRoutines.PracticeRoutineTree
{
    public class PracticeRoutineTreeViewModel : ViewModelBase
    {
        protected PracticeRoutine practiceRoutine;

        public PracticeRoutineTreeViewModel(PracticeRoutine practiceRoutine)
        {
            this.practiceRoutine = practiceRoutine ?? throw new ArgumentNullException("PracticeRoutine must be provided.");

            foreach (var timeSlot in practiceRoutine)
            {
                TimeSlotViewModel timeSlotViewModel = new TimeSlotViewModel(timeSlot);
                TimeSlots.Add(timeSlotViewModel);
            }

            AddCommand = new RelayCommand(() => Add(), () => true);
            RemoveSelectionCommand = new RelayCommand(() => RemoveSelection(), () => true);

            TimeSlots.ListChanged += (s, e) => RaisePropertyChanged(() => TotalTimeDisplay);
        }

        public string Title
        {
            get { return practiceRoutine.Title; }
            set
            {
                practiceRoutine.Title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public BindingList<TimeSlotViewModel> TimeSlots { get; } = new BindingList<TimeSlotViewModel>();

        public RelayCommand AddCommand { get; private set; }
        public RelayCommand RemoveSelectionCommand { get; private set; }

        public TimeSlotViewModel SelectedTimeSlot { get; set; }
        public string TotalTimeDisplay
        {
            get
            {
                return TimeFuncs.DisplayTimeFromSeconds(TimeSlots.Sum(ts => ts.AssignedSeconds));
            }
        }

        private void Add()
        {
            PracticeRoutineTimeSlot timeSlot = new PracticeRoutineTimeSlot("New Time Slot", 300, new List<TimeSlotExercise>());
            TimeSlots.Add(new TimeSlotViewModel(timeSlot));
        }

        private void RemoveSelection()
        {
            TimeSlots.Remove(SelectedTimeSlot);
        }
    }
}
