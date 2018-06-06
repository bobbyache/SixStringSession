using System;

namespace CygSoft.SmartSession.GoalManagement.Sessions
{
    public class DurationSessionResult : SessionResult
    {
        public DurationSessionResult(DateTime dateTime, DateTime endTime) : base(dateTime, endTime)
        {
        }

        public DurationSessionResult(string id, DateTime dateTime, DateTime endTime) : base(id, dateTime, endTime)
        {
        }
    }
}
