using System;

namespace CygSoft.SmartSession.GoalManagement
{
    public class MetronomeSessionResult : SessionResult
    {
        private int speed;

        public MetronomeSessionResult(DateTime dateTime, int minutes, int speed) : base(dateTime, minutes)
        {
            this.speed = speed;
        }

        public int Speed { get { return speed; } }
    }
}
