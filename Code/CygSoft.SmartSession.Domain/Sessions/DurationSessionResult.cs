using System;

namespace CygSoft.SmartSession.Domain.Sessions
{
    public class DurationSessionResult : SessionResult
    {
        public DurationSessionResult(DateTime dateTime, DateTime endTime) : base(dateTime, endTime)
        {
        }
    }
}
