using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSession.Domain.Records
{
    public class Session
    {
        public Session()
        {
            SessionTasks = new List<SessionPracticeTask>();
        }

        public int Id { get; set; }
        public DateTime StartTime { get; set; }

        // EndTime ???
        // there will never be an end time... this is not necessary
        // since duration can be inferred by lookin at this Session's practice durations.

        public string Notes { get; set; }

        public virtual List<SessionPracticeTask> SessionTasks { get; set; }
    }
}
