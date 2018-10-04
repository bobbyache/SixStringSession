using CygSoft.SmartSession.Domain.GoalTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Keywords
{
    public class GoalTaskKeyword
    {
        public int GoalTaskId { get; set; }
        public GoalTask GoalTask { get; set; }

        public int KeywordId { get; set; }
        public Keyword Keyword { get; set; }
    }
}
