using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public class ExerciseRecorder
    {
        private Timer timer = new Timer();
        private DateTime started;

        private double recordedSeconds;
        private Action tickActionFunc;

        public double Seconds
        {
            get
            {
                TimeSpan timeSpan = DateTime.Now - started;
                return recordedSeconds + timeSpan.TotalSeconds;
            }
        }

        public ExerciseRecorder(Action tickActionFunc)
        {
            timer.Interval = 1000;
            this.tickActionFunc = tickActionFunc;
        }

        private List<RecordingSlice> recordingSlices = new List<RecordingSlice>();

        public virtual void Start()
        {
            started = DateTime.Now;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            tickActionFunc();
        }

        public virtual void Pause()
        {
            timer.Stop();
            timer.Elapsed -= Timer_Elapsed;
            recordingSlices.Add(new RecordingSlice(started, DateTime.Now));
            recordedSeconds = recordingSlices.Sum(r => r.Seconds);
        }
    }
}
