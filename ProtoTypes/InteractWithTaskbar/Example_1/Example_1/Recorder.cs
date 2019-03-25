using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Example_1
{
    public class Recorder : IDisposable
    {
        internal class RecordingSlice
        {
            public DateTime StartTime { get; }
            public DateTime EndTime { get; }

            public double Seconds
            {
                get
                {
                    TimeSpan timeSpan = EndTime - StartTime;
                    return timeSpan.TotalSeconds;
                }
            }

            public RecordingSlice(DateTime startTime, DateTime endTime)
            {
                if (startTime > endTime)
                    throw new ArgumentOutOfRangeException("StartTime cannot be later than EndTime");

                StartTime = startTime;
                EndTime = endTime;
            }
        }

        private Timer timer = new Timer();
        private DateTime? recorderStartTime;
        private DateTime? recorderPauseTime;

        protected double recordedSeconds;

        protected Action tickActionFunc;
        public Action TickActionCallBack { set { tickActionFunc = value; } }

        public event EventHandler RecordingStatusChanged;
        public event EventHandler Tick;

        public double PreciseSeconds
        {
            get
            {
                if (recorderStartTime != null)
                {
                    if (Recording)
                    {
                        DateTime dateTime = recorderPauseTime ?? DateTime.Now;
                        TimeSpan timeSpan = dateTime - recorderStartTime.Value;

                        return recordedSeconds + timeSpan.TotalSeconds;
                    }
                    else
                    {
                        return recordedSeconds;
                    }
                }

                return recordedSeconds;
            }
        }

        public bool Recording { get { return timer.Enabled; } }

        public DateTime? StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }

        public string DisplayTime { get => TimeFuncs.DisplayTimeFromSeconds(PreciseSeconds); }

        public int Seconds { get => (int)Math.Round(PreciseSeconds, 0); }

        public Recorder()
        {
            timer.Interval = 1000;
            recorderPauseTime = null;
            recorderStartTime = null;
        }

        public virtual void Resume()
        {
            var startTime = DateTime.Now;
            if (StartTime == null) StartTime = startTime;

            recorderPauseTime = null;
            recorderStartTime = startTime;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            RecordingStatusChanged?.Invoke(this, new EventArgs());
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            tickActionFunc?.Invoke();
            Tick?.Invoke(this, new EventArgs());
        }

        public virtual void Pause()
        {
            timer.Stop();
            var endTime = DateTime.Now;
            recorderPauseTime = endTime;
            EndTime = endTime;
            var recordingSlice = new RecordingSlice(recorderStartTime.Value, recorderPauseTime.Value);
            recordedSeconds += recordingSlice.Seconds;

            RecordingStatusChanged?.Invoke(this, new EventArgs());
        }

        public virtual void Reset()
        {
            timer.Stop();
            timer.Elapsed -= Timer_Elapsed;
            StartTime = null;
            EndTime = null;
            recorderStartTime = null;
            recorderPauseTime = null;
            recordedSeconds = 0;

            RecordingStatusChanged?.Invoke(this, new EventArgs());
        }

        public void AddSeconds(int seconds)
        {
            if (!Recording)
            {
                recordedSeconds += seconds;
                tickActionFunc?.Invoke();
            }
        }

        public void SubstractSeconds(int seconds)
        {
            if (!Recording)
            {
                recordedSeconds -= seconds;
                if (recordedSeconds < 0) recordedSeconds = 0;

                tickActionFunc?.Invoke();
            }
        }

        public void AddMinutes(int minutes)
        {
            if (!Recording)
            {
                if (minutes <= 0)
                    return;

                int currentSeconds = (int)Math.Round(recordedSeconds, 0);
                int remainingSeconds = currentSeconds % 60;
                int fullMinutes = minutes - 1;
                currentSeconds += (fullMinutes * 60) + (60 - remainingSeconds);

                recordedSeconds = currentSeconds;

                tickActionFunc?.Invoke();
            }
        }

        public void SubtractMinutes(int minutes)
        {
            if (!Recording)
            {
                int currentSeconds = (int)Math.Round(recordedSeconds, 0);
                int remainingSeconds = currentSeconds % 60;

                if (currentSeconds == 0)
                {
                    recordedSeconds = 0;
                }
                else if (remainingSeconds == 0) // exactly on the minute (in seconds)
                {
                    recordedSeconds = currentSeconds -= (minutes * 60);
                }
                else
                {
                    int fullMinutes = minutes - 1;
                    double resultantSeconds = currentSeconds -= (fullMinutes * 60) + remainingSeconds;

                    if (resultantSeconds <= 0)
                        recordedSeconds = 0;
                    else
                        recordedSeconds = resultantSeconds;
                }

                tickActionFunc?.Invoke();
            }
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
                    timer.Dispose();
                }
            }
            isDisposed = true;
        }

        ~Recorder()
        {
            Dispose(false);
        }

        #endregion
    }
}
