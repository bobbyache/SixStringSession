using CygSoft.SmartSession.Domain.Goals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Keywords
{
    public class GoalKeyword
    {
        public int GoalId { get; set; }
        public Goal Goal { get; set; }

        public int KeywordId { get; set; }
        public Keyword Keyword { get; set; }
    }
}
