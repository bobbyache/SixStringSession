using System;

namespace CygSoft.SmartSession.Domain.Sessions
{
    public class PercentSessionResult : SessionResult
    {
        public double PercentCompleted { get; private set; }

        public PercentSessionResult(DateTime dateTime, DateTime endTime, double percent) : base(dateTime, endTime)
        {
            if (percent < 0)
                throw new ArgumentOutOfRangeException("Percent cannot be a negative value.");

            if (percent > 100)
                throw new ArgumentOutOfRangeException("Percent value cannot exceed 100.");

            this.PercentCompleted = percent;  
        }
    }
}
