using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Sessions;
using CygSoft.SmartSession.Infrastructure;
using System;

namespace CygSoft.SmartSession.Domain.RecordingRoutines
{
    public class RecordingExercise : IRecordingExercise, IDisposable
    {
        private class WeightedMetric : IWeightedEntity
        {
            private readonly double percentCompleted;

            public WeightedMetric(double percentComplete, int weighting)
            {
                percentCompleted = percentComplete;
                Weighting = weighting;
            }
            public int Weighting { get; private set; }

            public double PercentCompleted()
            {
                return percentCompleted;
            }
        }

        private IRecorder recorder;

        protected ISpeedProgress speedProgress;
        protected IPracticeTimeProgress practiceTimeProgress;
        protected IManualProgress manualProgress;

        public RecordingExercise(IRecorder recorder, string title, 
            ISpeedProgress speedProgress, IPracticeTimeProgress practiceTimeProgress, IManualProgress manualProgress)
        {
            Title = title;
            this.recorder = recorder ?? throw new ArgumentNullException("Recorder must be specified.");
            this.speedProgress = speedProgress ?? throw new ArgumentNullException("Progress element must be specified.");
            this.practiceTimeProgress = practiceTimeProgress ?? throw new ArgumentNullException("Progress element must be specified.");
            this.manualProgress = manualProgress ?? throw new ArgumentNullException("Progress element must be specified.");

            this.recorder.RecordingStatusChanged += Recorder_RecordingStatusChanged;
        }

        public bool Recording { get => recorder.Recording; }

        public double RecordedSeconds { get => recorder.PreciseSeconds; }

        public Action TickActionCallBack { set => recorder.TickActionCallBack = value; }

        public string RecordedSecondsDisplay { get => recorder.DisplayTime; }

        public string Title { get; private set; }

        public int? CurrentSpeed { get => speedProgress.CurrentSpeed; }

        public int CurrentTotalSeconds //{ get =>  practiceTimeProgress.CurrentTime + recorder.Seconds; }
        {
            get
            {
                var timeProgress = practiceTimeProgress.AddSeconds(recorder.Seconds);
                return timeProgress.CurrentTime;
            }
        }

        public double CurrentManualProgress { get => manualProgress.CalculateProgress(); }

        public double CurrentTimeProgress
        {
            get
            {
                var timeProgress = practiceTimeProgress.AddSeconds(recorder.Seconds);
                return timeProgress.CalculateProgress();
            }
        }

        public double CurrentSpeedProgress { get => speedProgress.CalculateProgress(); }

        public double CurrentOverAllProgress
        {
            get
            {
                var calculator = new WeightedProgressCalculator();
                calculator.Add(new WeightedMetric(manualProgress.CalculateProgress(), manualProgress.Weighting));
                calculator.Add(new WeightedMetric(speedProgress.CalculateProgress(), speedProgress.Weighting));
                calculator.Add(new WeightedMetric(practiceTimeProgress.CalculateProgress(), practiceTimeProgress.Weighting));

                return calculator.CalculateTotalProgress();
            }
        }

        public string TotalSecondsDisplay { get => TimeFuncs.DisplayTimeFromSeconds(CurrentTotalSeconds); }

        public event EventHandler RecordingStatusChanged;

        private void Recorder_RecordingStatusChanged(object sender, EventArgs e)
        {
            RecordingStatusChanged?.Invoke(this, new EventArgs());
        }


        public void AddMinutes(int minutes)
        {
            recorder.AddMinutes(minutes);
        }

        public void AddSeconds(int seconds)
        {
            recorder.AddSeconds(seconds);
        }

        public void Pause()
        {
            recorder.Pause();
        }

        public void Reset()
        {
            recorder.Reset();
        }

        public void Resume()
        {
            recorder.Resume();
        }

        public void SubstractSeconds(int seconds)
        {
            recorder.SubstractSeconds(seconds);
        }

        public void SubtractMinutes(int minutes)
        {
            recorder.SubtractMinutes(minutes);
        }

        #region Implement IDisposable

        private bool isDisposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // make sure we have not already been disposed!
            if (!isDisposed)
            {
                if (disposing)
                {
                    recorder.RecordingStatusChanged -= Recorder_RecordingStatusChanged;
                    recorder.Dispose();
                }
            }
            isDisposed = true;
        }

        ~RecordingExercise()
        {
            Dispose(false);
        }

        #endregion
    }
}
