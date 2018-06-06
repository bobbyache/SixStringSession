using System;

namespace CygSoft.SmartSession.GoalManagement.Sessions
{
    public class MetronomeSessionResult : SessionResult
    {
        private int speed;

        public MetronomeSessionResult(DateTime dateTime, DateTime endTime, int speed) : base(dateTime, endTime)
        {
            if (speed < 0)
                throw new ArgumentOutOfRangeException("Session metronome speed cannot be a negative value.");

            this.speed = speed;
        }

        public MetronomeSessionResult(string id, DateTime dateTime, DateTime endTime, int speed) : base(id, dateTime, endTime)
        {
            if (speed < 0)
                throw new ArgumentOutOfRangeException("Session metronome speed cannot be a negative value.");

            this.speed = speed;
        }

        public int Speed { get { return speed; } }
    }
}
