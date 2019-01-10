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

        public event EventHandler RecordingStatusChanged;

        public RelayCommand StartRecordingCommand { get; private set; }
        public RelayCommand PauseRecordingCommand { get; private set; }

        public RelayCommand CancelRecordingCommand { get; private set; }

        public RelayCommand SaveRecordingCommand { get; private set; }

        public string ExerciseTitle { get; private set; }

        public string TotalRecordedDisplayTime { get; private set; }

        public string CurrentSpeedInfo { get; private set; }

        protected IExerciseRecorder exerciseRecorder;

        private IExercise exercise;

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

        public double CurrentProgress
        {
            get
            {
                return exercise.GetPercentComplete();
            }
        }

        public string CurrentProgressText
        {
            get
            {
                return $"{Math.Round(exercise.GetPercentComplete(), 1)}% done.";
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

        private int metronomeSpeed;
        [Required]
        public int MetronomeSpeed
        {
            get
            {
                return metronomeSpeed;
            }
            set
            {
                Set(() => MetronomeSpeed, ref metronomeSpeed, value, true, true);
            }
        }

        private int manualProgress;
        [Range(0, 100, ErrorMessage = "Value must be between 0 and 100.")]
        public int ManualProgress
        {
            get { return manualProgress; }
            set
            {
                Set(() => ManualProgress, ref manualProgress, value, true, true);
            }
        }

        public ExerciseRecorderViewModel(IExerciseService exerciseService, IDialogViewService dialogService, IExerciseRecorder exerciseRecorder)
        {
            this.exerciseService = exerciseService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");
            this.exerciseRecorder = exerciseRecorder ?? throw new ArgumentNullException("Service must be provided.");

            this.exerciseRecorder.TickActionCallBack = Elapsed;

            ErrorsChanged += Exercise_ErrorsChanged;

            StartRecordingCommand = new RelayCommand(() => StartRecording(), () => !this.exerciseRecorder.Recording);
            PauseRecordingCommand = new RelayCommand(() => PauseRecording(), () => this.exerciseRecorder.Recording);
            CancelRecordingCommand = new RelayCommand(() => CancelRecording(), () => !this.exerciseRecorder.Recording);
            SaveRecordingCommand = new RelayCommand(() => SaveRecording(), CanExecuteSaveCommand);
        }

        private void ExerciseRecorder_RecordingStatusChanged(object sender, EventArgs e)
        {
            StartRecordingCommand.RaiseCanExecuteChanged();
            PauseRecordingCommand.RaiseCanExecuteChanged();

            RecordingStatusChanged?.Invoke(this, new EventArgs());
        }

        private bool CanExecuteSaveCommand()
        {
            var recordingStateValid = exerciseRecorder.Seconds > 0 && !exerciseRecorder.Recording;
            var speedMetricsStateValid = MetronomeSpeed >= 0;

            return recordingStateValid && speedMetricsStateValid;
        }

        private void Exercise_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveRecordingCommand.RaiseCanExecuteChanged();
            CancelRecordingCommand.RaiseCanExecuteChanged();
        }

        public void InitializeRecorder(int exerciseId)
        {
            this.exerciseRecorder.RecordingStatusChanged += ExerciseRecorder_RecordingStatusChanged;

            exercise = exerciseService.Get(exerciseId);
            exerciseRecorder.Reset();

            PauseButtonVisible = false;
            StartButtonVisible = true;

            this.MetronomeSpeed = 0;
            this.ManualProgress = exercise.GetLastRecordedManualProgress();
            this.ExerciseTitle = exercise.Title;
            this.CurrentSpeedInfo = $"Current: {exercise.GetLastRecordedSpeed()} bpm - Target: {exercise.TargetMetronomeSpeed ?? 0} bpm";
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

        internal void StartRecording()
        {
            if (!exerciseRecorder.Recording)
            {
                BusyIndicatorVisible = true;
                StartButtonVisible = false;
                PauseButtonVisible = true;

                exerciseRecorder.Resume();
                this.ValidateAll();
            }
        }

        internal void PauseRecording()
        {
            if (exerciseRecorder.Recording)
            {
                BusyIndicatorVisible = false;
                PauseButtonVisible = false;
                StartButtonVisible = true;
                
                exerciseRecorder.Pause();
                this.ValidateAll();
            }
        }

        private void CancelRecording()
        { 
            exerciseRecorder.RecordingStatusChanged -= ExerciseRecorder_RecordingStatusChanged;
            exerciseRecorder.Reset();

            Messenger.Default.Send(new CancelledExerciseRecordingMessage());
        }

        private void SaveRecording()
        {
            var exerciseActivity = exerciseService.CreateExerciseActivity(MetronomeSpeed, (int)exerciseRecorder.Seconds, ManualProgress, 
                exerciseRecorder.StartTime.Value, exerciseRecorder.EndTime.Value);
            exercise.ExerciseActivity.Add(exerciseActivity);

            exerciseService.Update(exercise);

            exerciseRecorder.RecordingStatusChanged -= ExerciseRecorder_RecordingStatusChanged;
            exerciseRecorder.Reset();
            Messenger.Default.Send(new SavedExerciseRecordingMessage(exercise.Id));
        }
    }
}
