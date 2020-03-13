using CygSoft.SmartSession.Desktop.Supports.Messages;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Desktop.Supports.Validators;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Recording;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CygSoft.SmartSession.Desktop.Exercises.Recorder
{
    public class SingleExerciseRecorderViewModel : ValidatableViewModel
    {
        private IExerciseService exerciseService;
        private readonly IDialogViewService dialogService;

        public event EventHandler RecordingStatusChanged;

        public RelayCommand StartRecordingCommand { get; private set; }
        public RelayCommand PauseRecordingCommand { get; private set; }

        public RelayCommand CancelRecordingCommand { get; private set; }

        public RelayCommand SaveRecordingCommand { get; private set; }

        public string ExerciseTitle { get; private set; }

        public string CurrentSpeedInfo { get; private set; }

        protected IExerciseRecorder exerciseRecorder;

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

        public double CurrentProgress { get => exerciseRecorder.CurrentOverAllProgress; }
        public string CurrentProgressText { get => $"{exerciseRecorder.CurrentOverAllProgress}% done."; }


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

        private string recordingTimeDisplay;
        public string RecordingTimeDisplay
        {
            get
            {
                return recordingTimeDisplay;
            }
            set
            {
                Set(() => RecordingTimeDisplay, ref recordingTimeDisplay, value);
            }
        }

        public string TotalRecordedDisplayTime
        {
            get { return exerciseRecorder.TotalSecondsDisplay; }
        }

        [Required]
        public int MetronomeSpeed
        {
            get { return exerciseRecorder.CurrentSpeed; }
            set
            {
                exerciseRecorder.CurrentSpeed = value;
                RaisePropertyChanged(() => MetronomeSpeed);
            }
        }

        [Range(0, 100, ErrorMessage = "Value must be between 0 and 100.")]
        public int ManualProgress
        {
            get { return (int)exerciseRecorder.CurrentManualProgress; }
            set
            {
                exerciseRecorder.CurrentManualProgress = value;
                RaisePropertyChanged(() => ManualProgress);
                RaisePropertyChanged(() => CurrentProgress);
                RaisePropertyChanged(() => CurrentProgressText);
            }
        }

        public SingleExerciseRecorderViewModel(IExerciseService exerciseService, IDialogViewService dialogService)
        {
            
            this.exerciseService = exerciseService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

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
            var recordingStateValid = exerciseRecorder.RecordedSeconds > 0 && !exerciseRecorder.Recording;
            var speedMetricsStateValid = MetronomeSpeed >= 0;

            return recordingStateValid && speedMetricsStateValid;
        }

        private void Exercise_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveRecordingCommand.RaiseCanExecuteChanged();
            CancelRecordingCommand.RaiseCanExecuteChanged();
        }

        public void InitializeRecorder(IExerciseRecorder exerciseRecorder)
        {
            if (this.exerciseRecorder != null)
            {
                this.exerciseRecorder.TickActionCallBack = null;
                this.exerciseRecorder.RecordingStatusChanged -= ExerciseRecorder_RecordingStatusChanged;
                this.exerciseRecorder.Dispose();
            }

            this.exerciseRecorder = exerciseRecorder;
            this.exerciseRecorder.TickActionCallBack = Elapsed;
            this.exerciseRecorder = exerciseRecorder ?? throw new ArgumentNullException("ExerciseRecorder must be provided.");
            this.exerciseRecorder.RecordingStatusChanged += ExerciseRecorder_RecordingStatusChanged;

            exerciseRecorder.Reset();

            PauseButtonVisible = false;
            StartButtonVisible = true;

            this.ExerciseTitle = exerciseRecorder.Title;
            this.CurrentSpeedInfo = $"Current: {exerciseRecorder.CurrentSpeed } bpm - Target: {exerciseRecorder.TargetSpeed} bpm";
            this.RecordingTimeDisplay = exerciseRecorder.RecordedSecondsDisplay;
        }

        private void Elapsed()
        {
            this.RecordingTimeDisplay = exerciseRecorder.RecordedSecondsDisplay;
            RaisePropertyChanged(() => TotalRecordedDisplayTime);
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
            exerciseRecorder.CurrentSpeed = MetronomeSpeed;
            exerciseRecorder.SaveRecording(exerciseService);

            exerciseRecorder.RecordingStatusChanged -= ExerciseRecorder_RecordingStatusChanged;
            exerciseRecorder.Reset();
            Messenger.Default.Send(new SavedExerciseRecordingMessage(exerciseRecorder.ExerciseId));
        }
    }
}
