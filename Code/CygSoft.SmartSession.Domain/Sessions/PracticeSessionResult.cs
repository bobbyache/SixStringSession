

using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.GoalTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Sessions
{
    public class PracticeSessionResult : Entity
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // The speed attained. Perhaps more thought here... what if you have an incrementing speed.
        // comfortable speed. speed attained. start speed.
        public int? Speed { get; set; }

        public int GetMinutesPracticed() { return (int)Math.Round(EndTime.Subtract(StartTime).TotalMinutes, 0); }
    }
}
