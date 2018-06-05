using System;

namespace CygSoft.SmartSession.GoalManagement
{
    public class PercentSessionResult : SessionResult
    {
        public double PercentCompleted { get; private set; }

        public PercentSessionResult(DateTime dateTime, int minutes, int percent) : base(dateTime, minutes)
        {
            if (percent < 0)
                throw new ArgumentOutOfRangeException("Percent cannot be a negative value.");

            if (percent > 100)
                throw new ArgumentOutOfRangeException("Percent value cannot exceed 100.");

            this.PercentCompleted = percent;  
        }
    }
}
