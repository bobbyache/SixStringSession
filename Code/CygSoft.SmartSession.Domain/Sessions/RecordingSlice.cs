using System;

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
}
