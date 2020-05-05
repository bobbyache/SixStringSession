using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Infrastructure;
using System;
using System.Linq;

namespace CygSoft.SmartSession.Domain.Recording
{
    public class ExerciseRecorder : IExerciseRecorder, IDisposable
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

        public ExerciseRecorder(IRecorder recorder, int exerciseId, string title,
            ISpeedProgress speedProgress, IPracticeTimeProgress practiceTimeProgress, IManualProgress manualProgress)
        {
            ExerciseId = exerciseId;
            Title = title;
            this.recorder = recorder ?? throw new ArgumentNullException("Recorder must be specified.");
            this.speedProgress = speedProgress ?? throw new ArgumentNullException("Progress element must be specified.");
            this.practiceTimeProgress = practiceTimeProgress ?? throw new ArgumentNullException("Progress element must be specified.");
            this.manualProgress = manualProgress ?? throw new ArgumentNullException("Progress element must be specified.");

            this.recorder.RecordingStatusChanged += Recorder_RecordingStatusChanged;
            this.recorder.Tick += TickEventFired;
        }


        public bool Recording { get => recorder.Recording; }

        public double RecordedSeconds { get => recorder.PreciseSeconds; }

        public Action TickActionCallBack { set => recorder.TickActionCallBack = value; }

        public string RecordedSecondsDisplay { get => recorder.DisplayTime; }

        public int ExerciseId { get; private set; }
        public string Title { get; private set; }

        public int CurrentSpeed
        {
            get => speedProgress.CurrentSpeed;
            set => speedProgress = speedProgress.NewSpeedSpeedProgress(value);
        }
        public int TargetSpeed { get => speedProgress.TargetSpeed; }

        public int CurrentTotalSeconds
        {
            get
            {
                var timeProgress = practiceTimeProgress.AddSeconds(recorder.Seconds);
                return timeProgress.CurrentTime;
            }
        }

        public int CurrentManualProgress
        {
            get { return manualProgress.CalculateProgress(); }
            set { manualProgress = manualProgress.NewManualProgress((int)value); }
        }

        public int CurrentTimeProgress
        {
            get
            {
                var timeProgress = practiceTimeProgress.AddSeconds(recorder.Seconds);
                return timeProgress.CalculateProgress();
            }
        }

        public int CurrentSpeedProgress { get => speedProgress.CalculateProgress(); }

        public int CurrentOverAllProgress
        {
            get
            {
                var calculator = new WeightedProgressCalculator();
                calculator.Add(new WeightedMetric(manualProgress.CalculateProgress(), manualProgress.Weighting));
                calculator.Add(new WeightedMetric(speedProgress.CalculateProgress(), speedProgress.Weighting));

                var timeProgress = practiceTimeProgress.AddSeconds(recorder.Seconds);
                calculator.Add(new WeightedMetric(timeProgress.CalculateProgress(), timeProgress.Weighting));

                return (int)Math.Round(calculator.CalculateTotalProgress());
            }
        }

        public string TotalSecondsDisplay { get => TimeFuncs.DisplayTimeFromSeconds(CurrentTotalSeconds); }

        public bool ProgressBySpeed => speedProgress.Weighting > 0;
        public bool ProgressByTime => practiceTimeProgress.Weighting > 0;
        public bool ProgressByManualInput => manualProgress.Weighting > 0;

        public double SpeedProgressPercentageAllocation { get => GetPercentageAllocationFor(speedProgress.Weighting); }

        public double TimeProgressPercentageAllocation { get => GetPercentageAllocationFor(practiceTimeProgress.Weighting); }

        public double ManualProgressPercentageAllocation { get => GetPercentageAllocationFor(manualProgress.Weighting); }

        public event EventHandler RecordingStatusChanged;
        public event EventHandler Tick;

        private void Recorder_RecordingStatusChanged(object sender, EventArgs e)
        {
            RecordingStatusChanged?.Invoke(this, new EventArgs());
        }

        protected virtual void TickEventFired(object sender, EventArgs e)
        {
            Tick?.Invoke(this, new EventArgs());
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

        public void IncrementSpeed(int ticks)
        {
            speedProgress = speedProgress.AddTicks(ticks);
        }

        public void DecrementSpeed(int ticks)
        {
            speedProgress = speedProgress.SubtractTicks(ticks);
        }

        public void IncrementManualProgress(int value)
        {
            manualProgress = manualProgress.Increase(value);
        }

        public void DecrementManualProgress(int value)
        {
            manualProgress = manualProgress.Decrease(value);
        }

        public void SaveRecording(IExerciseService exerciseService)
        {
            var exerciseActivity = exerciseService.CreateExerciseActivity(CurrentSpeed, (int)RecordedSeconds, (int)CurrentManualProgress);
            var exercise = exerciseService.Get(this.ExerciseId);

            exercise.ExerciseActivity.Add(exerciseActivity);
            exerciseService.Update(exercise);
        }

        private double GetPercentageAllocationFor(int weighting)
        {
            var values = new int[3] { manualProgress.Weighting, speedProgress.Weighting, practiceTimeProgress.Weighting };
            var sumOfItemWeightings = values.Sum();

            double percentage = (((double)weighting / sumOfItemWeightings) * 100);

            if (double.IsNaN(percentage))
                return 0;

            return Math.Round(percentage, 1);
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
                    recorder.Tick -= TickEventFired;
                    recorder.Dispose();
                }
            }
            isDisposed = true;
        }

        ~ExerciseRecorder()
        {
            Dispose(false);
        }

        #endregion
    }
}
