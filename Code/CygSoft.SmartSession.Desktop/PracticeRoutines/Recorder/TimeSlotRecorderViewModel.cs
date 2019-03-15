using CygSoft.SmartSession.Desktop.Exercises.Recorder;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Recording;
using CygSoft.SmartSession.Infrastructure;
using System;

namespace CygSoft.SmartSession.Desktop.PracticeRoutines.Recorder
{
    public class TimeSlotRecorderViewModel : ExerciseRecorderViewModel
    {
        public bool PracticeTimeCompleted { get => (int)TimeSlotExerciseRecorder.RemainingSeconds == 0;  }
        public int AssignedSeconds { get => (int)TimeSlotExerciseRecorder.AssignedSeconds; }
        public int RemainingSeconds { get => (int)TimeSlotExerciseRecorder.RemainingSeconds; }

        public int AssignedTimePercentage { get => (int)Math.Round((TimeSlotExerciseRecorder.RecordedSeconds / TimeSlotExerciseRecorder.AssignedSeconds) * 100, 0); }

        public string AssignedTimeDisplay { get => TimeFuncs.DisplayTimeFromSeconds(AssignedSeconds); }
        public string RemainingTimeDisplay { get => TimeFuncs.DisplayTimeFromSeconds(RemainingSeconds); }

        protected ITimeSlotExerciseRecorder TimeSlotExerciseRecorder { get => base.exerciseRecorder as ITimeSlotExerciseRecorder; }


        public TimeSlotRecorderViewModel(IExerciseService exerciseService, ITimeSlotExerciseRecorder timeSlotExerciseRecorder) : base(exerciseService, timeSlotExerciseRecorder)
        {
        }

        protected override void RaiseProgressPropertyChangedEvents()
        {
            base.RaiseProgressPropertyChangedEvents();
            RaisePropertyChanged(() => AssignedSeconds);
            RaisePropertyChanged(() => RemainingSeconds);
            RaisePropertyChanged(() => AssignedTimeDisplay);
            RaisePropertyChanged(() => RemainingTimeDisplay);
            RaisePropertyChanged(() => AssignedTimePercentage);
            RaisePropertyChanged(() => PracticeTimeCompleted);
        }
    }
}
