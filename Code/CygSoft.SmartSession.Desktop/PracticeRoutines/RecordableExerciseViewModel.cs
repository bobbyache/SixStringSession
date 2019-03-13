using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Recording;
using CygSoft.SmartSession.Infrastructure;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;

namespace CygSoft.SmartSession.Desktop.PracticeRoutines
{
    public class RecordableExerciseViewModel : ViewModelBase
    {
        private IExerciseRecorder exerciseRecorder;

        public string RecordingTimeDisplay
        {
            get { return exerciseRecorder.RecordedSecondsDisplay; }
        }

        private int initialManualProgress;
        public int InitialManualProgress
        {
            get
            {
                return initialManualProgress;
            }
            set
            {
                Set(() => InitialManualProgress, ref initialManualProgress, value);
            }
        }

        public int ManualProgress
        {
            get
            {
                return exerciseRecorder.CurrentManualProgress;
            }
            set
            {
                exerciseRecorder.CurrentManualProgress = value;
                RaiseProgressPropertyChangedEvents();
            }
        }

        public string ManualProgressInformationText
        {
            get
            {
                bool positive = ManualProgress > InitialManualProgress;
                double difference = Math.Abs(ManualProgress - InitialManualProgress);

                string positiveSign = positive ? "+" : "-";

                if (difference == 0)
                    return $"{ManualProgress}%";
                else
                    return $"{ManualProgress}% ({positiveSign}{difference})";
            }
        }

        public string MetronomeSpeedInformationText
        {
            get
            {
                bool positive = MetronomeSpeed > InitialMetronomeSpeed;
                double difference = Math.Abs(MetronomeSpeed - InitialMetronomeSpeed);

                string positiveSign = positive ? "+" : "-";

                if (difference == 0)
                    return $"{MetronomeSpeed}";
                else
                    return $"{MetronomeSpeed} ({positiveSign}{difference})";
            }
        }

        private int initialMetronomeSpeed;
        public int InitialMetronomeSpeed
        {
            get
            {
                return initialMetronomeSpeed;
            }
            set
            {
                Set(() => InitialMetronomeSpeed, ref initialMetronomeSpeed, value);
            }
        }

        public int MetronomeSpeed
        {
            get
            {
                return exerciseRecorder.CurrentSpeed;
            }
            set
            {
                exerciseRecorder.CurrentSpeed = value;
                RaiseProgressPropertyChangedEvents();
            }
        }

        private double seconds;
        public double Seconds
        {
            get
            {
                return seconds;
            }
            set
            {
                Set(() => Seconds, ref seconds, value);
            }
        }

        private string status;
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                Set(() => Status, ref status, value);
            }
        }

        private bool recording;
        public bool Recording
        {
            get
            {
                return recording;
            }
            set
            {
                Set(() => Recording, ref recording, value);
            }
        }


        public string Title => exerciseRecorder.Title;


        public int OverallProgress
        {
            get { return exerciseRecorder.CurrentOverAllProgress; }
        }


        public int SpeedProgress
        {
            get { return exerciseRecorder.CurrentSpeedProgress; }
        }

        public int PracticeTimeProgress
        {
            get { return exerciseRecorder.CurrentTimeProgress; }
        }

        public string TotalRecordedDisplayTime
        {
            get { return exerciseRecorder.TotalSecondsDisplay; }
        }

        public RelayCommand IncrementManualProgressCommand { get; private set; }
        public RelayCommand DecrementManualProgressCommand { get; private set; }

        public RelayCommand SmallSpeedDecrementCommand { get; private set; }

        public RelayCommand SmallSpeedIncrementCommand { get; private set; }

        public RelayCommand LargeSpeedDecrementCommand { get; private set; }

        public RelayCommand LargeSpeedIncrementCommand { get; private set; }

        public RelayCommand IncrementMinutesPracticedCommand { get; private set; }

        public RelayCommand IncrementSecondsPracticedCommand { get; private set; }

        public RelayCommand DecrementSecondsPracticedCommand { get; private set; }

        public RelayCommand DecrementMinutesPracticedCommand { get; private set; }

        public RecordableExerciseViewModel(IExerciseRecorder exerciseRecorder)
        {
            this.exerciseRecorder = exerciseRecorder;
            exerciseRecorder.TickActionCallBack = TickTock;
            exerciseRecorder.RecordingStatusChanged += ExerciseRecorder_RecordingStatusChanged;

            // OverallProgress = exerciseRecorder.CurrentOverAllProgress;

            Recording = exerciseRecorder.Recording;
            Status = exerciseRecorder.Recording ? "RECORDING..." : "";
            InitialManualProgress = exerciseRecorder.CurrentManualProgress;
            ManualProgress = InitialManualProgress;
            InitialMetronomeSpeed = exerciseRecorder.CurrentSpeed;
            MetronomeSpeed = InitialMetronomeSpeed;

            IncrementManualProgressCommand = new RelayCommand(() => IncrementManualProgress(), () => true);
            DecrementManualProgressCommand = new RelayCommand(() => DecrementManualProgress(), () => true);

            LargeSpeedDecrementCommand = new RelayCommand(() => DecrementMetronomeSpeedByTen(), () => true);
            LargeSpeedIncrementCommand = new RelayCommand(() => IncrementMetronomeSpeedByTen(), () => true);

            SmallSpeedDecrementCommand = new RelayCommand(() => DecrementMetronomeSpeed(), () => true);
            SmallSpeedIncrementCommand = new RelayCommand(() => IncrementMetronomeSpeed(), () => true);

            IncrementMinutesPracticedCommand = new RelayCommand(() => IncrementMinutesPracticed(), () => true);
            IncrementSecondsPracticedCommand = new RelayCommand(() => IncrementSecondsPracticed(), () => true);

            DecrementSecondsPracticedCommand = new RelayCommand(() => DecrementSecondsPracticed(), () => true);
            DecrementMinutesPracticedCommand = new RelayCommand(() => DecrementMinutesPracticed(), () => true);

            RaisePropertyChanged(() => Recording);
        }

        private void DecrementMetronomeSpeedByTen()
        {
            if (MetronomeSpeed > 10)
            {
                MetronomeSpeed -= 10;
            }
            else
                MetronomeSpeed = 0;
            RaiseProgressPropertyChangedEvents();
        }

        private void IncrementMetronomeSpeedByTen()
        {
            MetronomeSpeed += 10;
            RaiseProgressPropertyChangedEvents();
        }

        private void DecrementMinutesPracticed()
        {
            if (exerciseRecorder.Recording)
            {
                exerciseRecorder.Pause();
                exerciseRecorder.SubtractMinutes(1);
                exerciseRecorder.Resume();
            }
            else
                exerciseRecorder.SubtractMinutes(1);

            RaiseProgressPropertyChangedEvents();
        }

        private void DecrementSecondsPracticed()
        {
            if (exerciseRecorder.Recording)
            {
                exerciseRecorder.Pause();
                exerciseRecorder.SubstractSeconds(1);
                exerciseRecorder.Resume();
            }
            else
                exerciseRecorder.SubstractSeconds(1);

            RaiseProgressPropertyChangedEvents();
        }

        private void IncrementSecondsPracticed()
        {
            if (exerciseRecorder.Recording)
            {
                exerciseRecorder.Pause();
                exerciseRecorder.AddSeconds(1);
                exerciseRecorder.Resume();
            }
            else
                exerciseRecorder.AddSeconds(1);

            RaiseProgressPropertyChangedEvents();
        }

        private void IncrementMinutesPracticed()
        {
            if (exerciseRecorder.Recording)
            {
                exerciseRecorder.Pause();
                exerciseRecorder.AddMinutes(1);
                exerciseRecorder.Resume();
            }
            else
                exerciseRecorder.AddMinutes(1);

            RaiseProgressPropertyChangedEvents();
        }

        private void DecrementMetronomeSpeed()
        {
            if (MetronomeSpeed > 0)
            {
                MetronomeSpeed--;
                RaiseProgressPropertyChangedEvents();
            }
        }

        private void IncrementMetronomeSpeed()
        {
            MetronomeSpeed++;
            RaiseProgressPropertyChangedEvents();
        }

        private void IncrementManualProgress()
        {
            ManualProgress += 1;
        }

        private void DecrementManualProgress()
        {
            ManualProgress -= 1;
        }

        private void RaiseProgressPropertyChangedEvents()
        {
            RaisePropertyChanged(() => SpeedProgress);
            RaisePropertyChanged(() => OverallProgress);
            RaisePropertyChanged(() => RecordingTimeDisplay);
            RaisePropertyChanged(() => TotalRecordedDisplayTime);
            RaisePropertyChanged(() => PracticeTimeProgress);

            RaisePropertyChanged(() => MetronomeSpeed);
            RaisePropertyChanged(() => MetronomeSpeedInformationText);
            RaisePropertyChanged(() => ManualProgress);
            RaisePropertyChanged(() => ManualProgressInformationText);
        }

        private void ExerciseRecorder_RecordingStatusChanged(object sender, EventArgs e)
        {
            Status = exerciseRecorder.Recording ? "RECORDING..." : "";
            Recording = exerciseRecorder.Recording;
        }

        private void TickTock()
        {
            Seconds = exerciseRecorder.RecordedSeconds;
            RaiseProgressPropertyChangedEvents();
        }

        public void Start()
        {
            if (!exerciseRecorder.Recording)
            {
                exerciseRecorder.Resume();
            }
        }

        internal void Pause()
        {
            if (exerciseRecorder.Recording)
            {
                exerciseRecorder.Pause();
            }
        }

        private void Reset()
        {
            exerciseRecorder.Reset();
        }

        public void SaveRecording(IExerciseService exerciseService)
        {
            exerciseRecorder.SaveRecording(exerciseService);
        }
    }
}
