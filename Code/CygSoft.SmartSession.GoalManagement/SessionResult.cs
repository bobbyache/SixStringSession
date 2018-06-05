using System;

namespace CygSoft.SmartSession.GoalManagement
{
    public abstract class SessionResult
    {
        private DateTime dateTime;
        private int minutes;

        public SessionResult(DateTime dateTime, int minutes)
        {
            if (minutes < 0)
                throw new ArgumentOutOfRangeException("Session minutes cannot be a negative value.");

            this.dateTime = dateTime;
            this.minutes = minutes;
        }

        public int Minutes { get { return minutes; } }
        public DateTime DateCreated { get { return this.dateTime; } }
    }
}
