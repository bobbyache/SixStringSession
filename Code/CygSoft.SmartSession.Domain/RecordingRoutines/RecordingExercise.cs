using CygSoft.SmartSession.Domain.Sessions;
using System;

namespace CygSoft.SmartSession.Domain.RecordingRoutines
{
    public class RecordingExercise : IRecordingExercise, IDisposable
    {
        private IRecorder recorder;

        public RecordingExercise(IRecorder recorder)
        {
            this.recorder = recorder ?? throw new ArgumentNullException("Recorder must be specified.");
            this.recorder.RecordingStatusChanged += Recorder_RecordingStatusChanged;
        }

        public bool Recording { get => recorder.Recording; }

        public double RecordedSeconds { get => recorder.Seconds; }

        public Action TickActionCallBack { set => recorder.TickActionCallBack = value; }

        public string RecordedSecondsDisplay { get => recorder.DisplayTime; }

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
