using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.DomainLegacy
{
    public class SessionPracticeTaskDuration
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // or (either one or the other must be applied)

        public int Minutes { get; set; }
    }
}
