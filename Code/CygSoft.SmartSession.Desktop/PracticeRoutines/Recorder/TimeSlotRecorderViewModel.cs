using CygSoft.SmartSession.Desktop.Exercises.Recorder;
using CygSoft.SmartSession.Domain.Recording;
using CygSoft.SmartSession.Infrastructure;

namespace CygSoft.SmartSession.Desktop.PracticeRoutines.Recorder
{
    public class TimeSlotRecorderViewModel : RecorderViewModel
    {
        public int AssignedSeconds { get => (int)TimeSlotExerciseRecorder.AssignedSeconds; }
        public int RemainingSeconds { get => (int)TimeSlotExerciseRecorder.RemainingSeconds; }

        public string AssignedTimeDisplay { get => TimeFuncs.DisplayTimeFromSeconds(AssignedSeconds); }
        public string RemainingTimeDisplay { get => TimeFuncs.DisplayTimeFromSeconds(RemainingSeconds); }

        protected ITimeSlotExerciseRecorder TimeSlotExerciseRecorder { get => base.exerciseRecorder as ITimeSlotExerciseRecorder; }


        public TimeSlotRecorderViewModel(ITimeSlotExerciseRecorder timeSlotExerciseRecorder) : base(timeSlotExerciseRecorder)
        {
        }

        protected override void RaiseProgressPropertyChangedEvents()
        {
            base.RaiseProgressPropertyChangedEvents();
            RaisePropertyChanged(() => AssignedSeconds);
            RaisePropertyChanged(() => RemainingSeconds);
            RaisePropertyChanged(() => AssignedTimeDisplay);
            RaisePropertyChanged(() => RemainingTimeDisplay);
        }
    }
}
