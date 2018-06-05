using System;

namespace CygSoft.SmartSession.GoalManagement
{
    public abstract class SessionResult
    {
        private DateTime startDate;
        private int minutes;

        public SessionResult(DateTime startDate, int minutes)
        {
            if (minutes < 0)
                throw new ArgumentOutOfRangeException("Session minutes cannot be a negative value.");

            this.startDate = startDate;
            this.minutes = minutes;
        }

        public int Minutes { get { return minutes; } }
        public DateTime StartDate { get { return this.startDate; } }
    }
}
