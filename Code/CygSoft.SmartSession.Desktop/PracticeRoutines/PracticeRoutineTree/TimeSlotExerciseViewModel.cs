using CygSoft.SmartSession.Domain.PracticeRoutines;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.PracticeRoutines.PracticeRoutineTree
{
    public class TimeSlotExerciseViewModel : TreeViewItemViewModel
    {
        private TimeSlotViewModel timeSlotViewModel;
        private TimeSlotExercise timeSlotExercise;

        public TimeSlotExerciseViewModel(TimeSlotExercise timeSlotExercise, TimeSlotViewModel timeSlotViewModel)
        {
            this.timeSlotExercise = timeSlotExercise ?? throw new ArgumentNullException("Time Slot Exercise must be provided.");
            this.timeSlotViewModel = timeSlotViewModel ?? throw new ArgumentNullException("Time Slot View Model must be provided.");
        }

        public string Title => timeSlotExercise.Title;

        public int FrequencyWeighting
        {
            get
            {
                return timeSlotExercise.FrequencyWeighting;
            }
            set
            {
                timeSlotExercise.FrequencyWeighting = value;
                RaisePropertyChanged(() => FrequencyWeighting);
            }
        }

        public TimeSlotViewModel TimeSlotViewModel => timeSlotViewModel;
        public TimeSlotExercise TimeSlotExercise => timeSlotExercise;
    }
}
