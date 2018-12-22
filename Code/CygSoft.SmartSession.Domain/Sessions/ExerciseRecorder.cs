using CygSoft.SmartSession.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace CygSoft.SmartSession.Domain.Sessions
{
    public class ExerciseRecorder : IExerciseRecorder
    {
        private class RecordingSlice
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

        private double recordedSeconds;

        private Action tickActionFunc;
        public Action TickActionCallBack { set { tickActionFunc = value; } }

        public event EventHandler RecordingStatusChanged;

        public double Seconds
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

                return 0;
            }
        }

        public bool Recording { get { return timer.Enabled; } }

        public DateTime? StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }

        public string DisplayTime { get => TimeFuncs.DisplayTimeFromSeconds(Seconds); }

        public ExerciseRecorder()
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
    }
}
