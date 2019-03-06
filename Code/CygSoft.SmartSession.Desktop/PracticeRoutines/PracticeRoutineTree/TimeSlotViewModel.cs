using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Infrastructure;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
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
    }
}
