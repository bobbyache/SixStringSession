using System;

namespace CygSoft.SmartSession.GoalManagement
{
    public abstract class SessionResult
    {
        private DateTime dateTime;
        private int minutes;

        public SessionResult(DateTime dateTime, int minutes)
        {
            this.dateTime = dateTime;
            this.minutes = minutes;
        }

        public int Minutes { get { return minutes; } }
        public DateTime DateCreated { get { return this.dateTime; } }
    }
}
