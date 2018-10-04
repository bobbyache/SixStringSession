using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.DomainLegacy
{
    public class SessionPracticeTaskMetronome
    {
        public int Id { get; set; }
        public int StartSpeed { get; set; }
        public int EndSpeed { get; set; }
        public int ComfortableSpeed { get; set; } // perhaps always default to StartSpeed if not entered. Basically, the speed i'm currently comfortable with.

    }
}
