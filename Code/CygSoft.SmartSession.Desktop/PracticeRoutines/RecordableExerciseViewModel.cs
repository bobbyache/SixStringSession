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
        public IExercise Exercise { get; set; }
        private IRecorder exerciseRecorder;

        private string displayTime;
        public string DisplayTime
        {
            get
            {
                return displayTime;
            }
            set
            {
                Set(() => DisplayTime, ref displayTime, value);
            }
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

        private int manualProgress;
        public int ManualProgress
        {
            get
            {
                return manualProgress;
            }
            set
            {
                Set(() => ManualProgress, ref manualProgress, value);
                RaisePropertyChanged("ManualProgressInformationText");
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

        private int metronomeSpeed;
        public int MetronomeSpeed
        {
            get
            {
                return metronomeSpeed;
            }
            set
            {
                Set(() => MetronomeSpeed, ref metronomeSpeed, value);
                RaisePropertyChanged("MetronomeSpeedInformationText");
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

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                Set(() => Title, ref title, value);
            }
        }

        private int overallProgress;
        public int OverallProgress
        {
            get
            {
                return overallProgress;
            }
            set
            {
                Set(() => OverallProgress, ref overallProgress, value);
            }
        }

        private int speedProgress;
        public int SpeedProgress
        {
            get
            {
                return speedProgress;
            }
            set
            {
                Set(() => SpeedProgress, ref speedProgress, value);
            }
        }

        private int practiceTimeProgress;
        public int PracticeTimeProgress
        {
            get
            {
                return practiceTimeProgress;
            }
            set
            {
                Set(() => PracticeTimeProgress, ref practiceTimeProgress, value);
            }
        }

        private string totalPracticeTime;
        public string TotalPracticeTime
        {
            get
            {
                return totalPracticeTime;
            }
            set
            {
                Set(() => TotalPracticeTime, ref totalPracticeTime, value);
            }
        }

        public RelayCommand IncrementManualProgressCommand { get; private set; }
        public RelayCommand DecrementManualProgressCommand { get; private set; }

        public RelayCommand DecrementMetronomeSpeedCommand { get; private set; }

        public RelayCommand IncrementMetronomeSpeedCommand { get; private set; }

        public RelayCommand DecrementMetronomeSpeedByTenCommand { get; private set; }

        public RelayCommand IncrementMetronomeSpeedByTenCommand { get; private set; }

        public RelayCommand IncrementMinutesPracticedCommand { get; private set; }

        public RelayCommand IncrementSecondsPracticedCommand { get; private set; }

        public RelayCommand DecrementSecondsPracticedCommand { get; private set; }

        public RelayCommand DecrementMinutesPracticedCommand { get; private set; }

        public RecordableExerciseViewModel(IExercise exercise)
        {
            this.Exercise = exercise;
            exerciseRecorder = new Recorder();
            exerciseRecorder.TickActionCallBack = TickTock;
            exerciseRecorder.RecordingStatusChanged += ExerciseRecorder_RecordingStatusChanged;

            OverallProgress = (int)Math.Round(exercise.GetPercentComplete(), 0);
            SpeedProgress = (int)Math.Round(exercise.GetSpeedProgress(), 0);
            PracticeTimeProgress = (int)Math.Round(exercise.GetPracticeTimeProgress(), 0);

            TotalPracticeTime = TimeFuncs.DisplayTimeFromSeconds(exercise.GetSecondsPracticed());

            Title = exercise.Title;
            Recording = exerciseRecorder.Recording;
            Status = exerciseRecorder.Recording ? "RECORDING..." : "";
            DisplayTime = exerciseRecorder.DisplayTime;
            InitialManualProgress = exercise.GetLastRecordedManualProgress();
            ManualProgress = InitialManualProgress;
            InitialMetronomeSpeed = exercise.GetLastRecordedSpeed();
            MetronomeSpeed = InitialMetronomeSpeed;

            IncrementManualProgressCommand = new RelayCommand(() => IncrementManualProgress(), () => true);
            DecrementManualProgressCommand = new RelayCommand(() => DecrementManualProgress(), () => true);

            DecrementMetronomeSpeedByTenCommand = new RelayCommand(() => DecrementMetronomeSpeedByTen(), () => true);
            IncrementMetronomeSpeedByTenCommand = new RelayCommand(() => IncrementMetronomeSpeedByTen(), () => true);

            DecrementMetronomeSpeedCommand = new RelayCommand(() => DecrementMetronomeSpeed(), () => true);
            IncrementMetronomeSpeedCommand = new RelayCommand(() => IncrementMetronomeSpeed(), () => true);

            IncrementMinutesPracticedCommand = new RelayCommand(() => IncrementMinutesPracticed(), () => true);
            IncrementSecondsPracticedCommand = new RelayCommand(() => IncrementSecondsPracticed(), () => true);

            DecrementSecondsPracticedCommand = new RelayCommand(() => DecrementSecondsPracticed(), () => true);
            DecrementMinutesPracticedCommand = new RelayCommand(() => DecrementMinutesPracticed(), () => true);

        }

        private void DecrementMetronomeSpeedByTen()
        {
            if (MetronomeSpeed > 10)
            {
                MetronomeSpeed -= 10;
            }
        }

        private void IncrementMetronomeSpeedByTen()
        {
            MetronomeSpeed += 10;
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
        }

        private void DecrementMetronomeSpeed()
        {
            if (MetronomeSpeed > 0)
            {
                MetronomeSpeed--;
            }
        }

        private void IncrementMetronomeSpeed()
        {
            MetronomeSpeed++;
        }

        private void IncrementManualProgress()
        {
            ManualProgress += 1;
        }

        private void DecrementManualProgress()
        {
            ManualProgress -= 1;
        }

        private void ExerciseRecorder_RecordingStatusChanged(object sender, EventArgs e)
        {
            Status = exerciseRecorder.Recording ? "RECORDING..." : "";
            Recording = exerciseRecorder.Recording;
        }

        private void TickTock()
        {
            Seconds = exerciseRecorder.PreciseSeconds;
            DisplayTime = exerciseRecorder.DisplayTime;
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
    }
}
