using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace CygSoft.SmartSession.Domain.Sessions
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

    public class ExerciseRecorder : IExerciseRecorder
    {
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
                    DateTime dateTime = recorderPauseTime ?? DateTime.Now;
                    TimeSpan timeSpan = dateTime - recorderStartTime.Value;

                    if (Recording)
                    {
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

        public DateTime StartTime { get { return recordingSlices.Min(r => r.StartTime); } }
        public DateTime EndTime { get { return recordingSlices.Max(r => r.EndTime); } }

        public ExerciseRecorder()
        {
            timer.Interval = 1000;
        }

        private List<RecordingSlice> recordingSlices = new List<RecordingSlice>();

        public virtual void Start()
        {
            recorderPauseTime = null;
            recorderStartTime = DateTime.Now;
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
            recorderPauseTime = DateTime.Now;
            recordingSlices.Add(new RecordingSlice(recorderStartTime.Value, recorderPauseTime.Value));
            recordedSeconds = recordingSlices.Sum(r => r.Seconds);

            RecordingStatusChanged?.Invoke(this, new EventArgs());
        }

        public virtual void Clear()
        {
            timer.Stop();
            timer.Elapsed -= Timer_Elapsed;
            recordingSlices.Clear();
            recorderStartTime = null;
            recorderPauseTime = null;
            recordedSeconds = 0;

            RecordingStatusChanged?.Invoke(this, new EventArgs());
        }
    }
}
