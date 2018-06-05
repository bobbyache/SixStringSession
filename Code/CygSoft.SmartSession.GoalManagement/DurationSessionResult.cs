using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement
{
    public class DurationSessionResult
    {
        private DateTime dateTime;
        private int minutes;

        public DurationSessionResult(DateTime dateTime, int minutes)
        {
            this.dateTime = dateTime;
            this.minutes = minutes;
        }

        public int Minutes { get { return minutes; } }
    }
}
