using System;

namespace CygSoft.SmartSession.DomainLegacy
{
    public class MetronomeSessionResult : SessionResult
    {
        public int Speed { get; }

        public MetronomeSessionResult(DateTime dateTime, DateTime endTime, int speed) : base(dateTime, endTime)
        {
            if (speed < 0)
                throw new ArgumentOutOfRangeException("Session metronome speed cannot be a negative value.");

            Speed = speed;
        }
    }
}
