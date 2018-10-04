using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.DomainLegacy
{
    public class SessionPracticeTask
    {
        public int PracticeTaskId { get; set; }
        public int SessionId { get; set; }

        public PracticeTask PracticeTask { get; set; }
        public Session Session { get; set; }

        public SessionPracticeTaskMetronome Metronome { get; set; }

        public SessionPracticeTaskDuration Duration { get; set; }

        public SessionPracticeTaskManualProgress ManualProgressEstimate { get; set; }
    }
}
