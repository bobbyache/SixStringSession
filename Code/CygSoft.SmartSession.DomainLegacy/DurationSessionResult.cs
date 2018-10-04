using System;

namespace CygSoft.SmartSession.DomainLegacy
{
    public class DurationSessionResult : SessionResult
    {
        public DurationSessionResult(DateTime dateTime, DateTime endTime) : base(dateTime, endTime)
        {
        }
    }
}
