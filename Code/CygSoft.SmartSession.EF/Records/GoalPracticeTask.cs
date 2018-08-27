using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSession.Domain.Records
{
    public class GoalPracticeTask
    {
        public int GoalId { get; set; }
        public int TaskId { get; set; }
        public Goal Goal { get; set; }
        public PracticeTask Task { get; set; }
    }
}
