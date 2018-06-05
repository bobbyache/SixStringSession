using System;

namespace CygSoft.SmartSession.GoalManagement
{
    public class MetronomeSessionResult : SessionResult
    {
        private int speed;

        public MetronomeSessionResult(DateTime dateTime, int minutes, int speed) : base(dateTime, minutes)
        {
            if (speed < 0)
                throw new ArgumentOutOfRangeException("Session metronome speed cannot be a negative value.");

            this.speed = speed;
        }

        public int Speed { get { return speed; } }
    }
}
