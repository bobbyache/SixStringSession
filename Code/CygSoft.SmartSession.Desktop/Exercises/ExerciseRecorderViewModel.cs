using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Sessions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseRecorderViewModel : ViewModelBase
    {
        private IExerciseService exerciseService;
        private IDialogViewService dialogService;

        public RelayCommand StartRecordingCommand { get; private set; }
        public RelayCommand PauseRecordingCommand { get; private set; }

        public RelayCommand CancelRecordingCommand { get; private set; }

        public RelayCommand SaveRecordingCommand { get; private set; }

        public string ExerciseTitle { get; private set; }

        public string TotalRecordedDisplayTime { get; private set; }

        public string CurrentSpeedInfo { get; private set; }

        protected bool timing = false;
        protected ExerciseRecorder exerciseRecorder;

        private Exercise exercise;

        private string activityRecordedDisplayTime;
        public string ActivityRecordedDisplayTime
        {
            get
            {
                return activityRecordedDisplayTime;
            }
            set
            {
                Set(() => ActivityRecordedDisplayTime, ref activityRecordedDisplayTime, value);
            }
        }

        private int startSpeed;
        public int StartSpeed
        {
            get
            {
                return startSpeed;
            }
            set
            {
                Set(() => StartSpeed, ref startSpeed, value);
            }
        }

        private int comfortSpeed;
        public int ComfortSpeed
        {
            get
            {
                return comfortSpeed;
            }
            set
            {
                Set(() => ComfortSpeed, ref comfortSpeed, value);
            }
        }

        private int highestSpeed;
        public int HighestSpeed
        {
            get
            {
                return highestSpeed;
            }
            set
            {
                Set(() => HighestSpeed, ref highestSpeed, value);
            }
        }

        public ExerciseRecorderViewModel(IExerciseService exerciseService, IDialogViewService dialogService)
        {
            this.exerciseService = exerciseService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            StartRecordingCommand = new RelayCommand(() => StartRecording(), () => true);
            PauseRecordingCommand = new RelayCommand(() => PauseRecording(), () => true);
            CancelRecordingCommand = new RelayCommand(() => CancelRecording(), () => true);
            SaveRecordingCommand = new RelayCommand(() => SaveRecording(), () => true);
        }

        public void BeginRecordingExercise(int exerciseId)
        {
            exerciseRecorder = new ExerciseRecorder(Elapsed);

            exercise = exerciseService.Get(exerciseId);

            this.ComfortSpeed = 0;
            this.HighestSpeed = 0;
            this.StartSpeed = exercise.GetCurrentComfortSpeed();
            this.ExerciseTitle = exercise.Title;
            this.CurrentSpeedInfo = $"Current: {exercise.GetCurrentComfortSpeed()} bpm - Target: {exercise.TargetMetronomeSpeed ?? 0} bpm";
            this.ActivityRecordedDisplayTime = "00:00:00";
            this.TotalRecordedDisplayTime = DisplayTime(exercise.GetSecondsPracticed());
        }

        private void Elapsed()
        {
            this.ActivityRecordedDisplayTime = DisplayTime(exerciseRecorder.Seconds);
        }

        private string DisplayTime(double seconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            return t.ToString(@"hh\:mm\:ss");
        }

        private void StartRecording()
        {
            if (!timing)
            {
                exerciseRecorder.Start();
            }
            timing = true;
        }

        private void PauseRecording()
        {
            if (timing)
            {
                exerciseRecorder.Pause();
            }
            timing = false;
        }

        private void CancelRecording()
        {
            exerciseRecorder.Clear();
            Messenger.Default.Send(new CancelledExerciseRecordingMessage());
        }

        private void SaveRecording()
        {
            SessionExerciseActivity exerciseActivity = new SessionExerciseActivity();
            exerciseActivity.DateCreated = DateTime.Now;
            exerciseActivity.DateModified = exerciseActivity.DateCreated;
            exerciseActivity.AchievedMetronomeSpeed = HighestSpeed;
            exerciseActivity.ComfortMetronomeSpeed = ComfortSpeed;
            exerciseActivity.StartMetronomeSpeed = StartSpeed;
            exerciseActivity.ExerciseId = exercise.Id;
            exerciseActivity.Seconds = (int)exerciseRecorder.Seconds;
            exerciseActivity.StartTime = exerciseRecorder.StartTime;
            exerciseActivity.EndTime = exerciseRecorder.EndTime;

            exercise.ExerciseActivity.Add(exerciseActivity);
            exerciseService.Update(exercise);

            exerciseRecorder.Clear();
            Messenger.Default.Send(new SavedExerciseRecordingMessage(exercise.Id));
        }

    }
}
