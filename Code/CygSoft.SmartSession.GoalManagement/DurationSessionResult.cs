using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement
{
    public class DurationSessionResult : SessionResult
    {
        public DurationSessionResult(DateTime dateTime, int minutes) : base(dateTime, minutes)
        {
        }
    }
}
