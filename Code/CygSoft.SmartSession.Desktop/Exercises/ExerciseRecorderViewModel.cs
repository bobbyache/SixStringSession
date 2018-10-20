using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Desktop.Supports.Validators;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Sessions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseRecorderViewModel : ValidatableViewModel
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

        private bool startButtonVisible;
        public bool StartButtonVisible
        {
            get
            {
                return startButtonVisible;
            }
            set
            {
                Set(() => StartButtonVisible, ref startButtonVisible, value);
            }
        }

        private bool busyIndicatorVisible;
        public bool BusyIndicatorVisible
        {
            get
            {
                return busyIndicatorVisible;
            }
            set
            {
                Set(() => BusyIndicatorVisible, ref busyIndicatorVisible, value);
            }
        }

        private bool pauseButtonVisible;
        public bool PauseButtonVisible
        {
            get
            {
                return pauseButtonVisible;
            }
            set
            {
                Set(() => PauseButtonVisible, ref pauseButtonVisible, value);
            }
        }

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
        [Required]
        public int StartSpeed
        {
            get
            {
                return startSpeed;
            }
            set
            {
                Set(() => StartSpeed, ref startSpeed, value, true, true);
            }
        }

        private int comfortSpeed;
        [Required]
        public int ComfortSpeed
        {
            get
            {
                return comfortSpeed;
            }
            set
            {
                Set(() => ComfortSpeed, ref comfortSpeed, value, true, true);
            }
        }

        private int highestSpeed;
        [Required]
        public int HighestSpeed
        {
            get
            {
                return highestSpeed;
            }
            set
            {
                Set(() => HighestSpeed, ref highestSpeed, value, true, true);
            }
        }

        public ExerciseRecorderViewModel(IExerciseService exerciseService, IDialogViewService dialogService)
        {
            this.exerciseService = exerciseService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            ErrorsChanged += Exercise_ErrorsChanged;
            
            StartRecordingCommand = new RelayCommand(() => StartRecording(), () => !exerciseRecorder.Recording);
            PauseRecordingCommand = new RelayCommand(() => PauseRecording(), () => exerciseRecorder.Recording);
            CancelRecordingCommand = new RelayCommand(() => CancelRecording(), () => !exerciseRecorder.Recording);
            SaveRecordingCommand = new RelayCommand(() => SaveRecording(), CanExecuteSaveCommand);
        }

        private void ExerciseRecorder_RecordingStatusChanged(object sender, EventArgs e)
        {
            StartRecordingCommand.RaiseCanExecuteChanged();
            PauseRecordingCommand.RaiseCanExecuteChanged();
        }

        private bool CanExecuteSaveCommand()
        {
            var recordingStateValid = exerciseRecorder.Seconds > 0 && !exerciseRecorder.Recording;
            var speedMetricsStateValid = StartSpeed > 0 && ComfortSpeed > 0 && HighestSpeed > 0 &&  (HighestSpeed >= StartSpeed);

            return recordingStateValid && speedMetricsStateValid;
        }

        private void Exercise_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveRecordingCommand.RaiseCanExecuteChanged();
            CancelRecordingCommand.RaiseCanExecuteChanged();
        }

        public void BeginRecordingExercise(int exerciseId)
        {
            exerciseRecorder = new ExerciseRecorder(Elapsed);
            exerciseRecorder.RecordingStatusChanged += ExerciseRecorder_RecordingStatusChanged;

            exercise = exerciseService.Get(exerciseId);

            PauseButtonVisible = false;
            StartButtonVisible = true;

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
                BusyIndicatorVisible = true;
                StartButtonVisible = false;
                PauseButtonVisible = true;

                exerciseRecorder.Start();
                this.ValidateAll();
            }
            timing = true;
        }

        private void PauseRecording()
        {
            if (timing)
            {
                BusyIndicatorVisible = false;
                PauseButtonVisible = false;
                StartButtonVisible = true;
                
                exerciseRecorder.Pause();
                this.ValidateAll();
            }
            timing = false;
        }

        private void CancelRecording()
        { 
            exerciseRecorder.RecordingStatusChanged -= ExerciseRecorder_RecordingStatusChanged;
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

            exerciseRecorder.RecordingStatusChanged -= ExerciseRecorder_RecordingStatusChanged;
            exerciseRecorder.Clear();
            Messenger.Default.Send(new SavedExerciseRecordingMessage(exercise.Id));
        }
    }
}
