using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Sessions
{
    public class SessionExerciseActivity : Entity
    {
        public int StartMetronomeSpeed { get; set; }
        public int ComfortMetronomeSpeed { get; set; }
        public int AchievedMetronomeSpeed { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int ExerciseId { get; set; }
    
        public int GetSecondsPracticed()
        {
            throw new NotImplementedException();
        }
    }
}
