using CygSoft.SmartSession.Domain.Sessions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseRecorderViewModel : ViewModelBase
    {
        public RelayCommand StartRecordingCommand { get; private set; }
        public RelayCommand PauseRecordingCommand { get; private set; }

        public RelayCommand CancelRecordingCommand { get; private set; }

        public RelayCommand SaveRecordingCommand { get; private set; }

        public string ExerciseTitle { get; private set; }

        public string TotalRecordedDisplayTime { get; private set; }

        public string CurrentSpeedInfo { get; private set; }

        protected bool timing = false;
        protected ExerciseRecorder exerciseRecorder;

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

        public ExerciseRecorderViewModel()
        {
            this.exerciseRecorder = new ExerciseRecorder(Elapsed);

            StartRecordingCommand = new RelayCommand(() => StartRecording(), () => true);
            PauseRecordingCommand = new RelayCommand(() => PauseRecording(), () => true);
            CancelRecordingCommand = new RelayCommand(() => CancelRecording(), () => true);
            SaveRecordingCommand = new RelayCommand(() => SaveRecording(), () => true);

            this.comfortSpeed = 82;
            this.highestSpeed = 93;
            this.startSpeed = 75;
            this.ExerciseTitle = "Metallica - Fade to Black - Bars 1 - 10";
            this.CurrentSpeedInfo = "Current: 105 bpm - Target: 160 bpm";
            this.ActivityRecordedDisplayTime = "00:00:00";
            this.TotalRecordedDisplayTime = "01:22:32";
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

        }

        private void SaveRecording()
        {

        }

    }
}
