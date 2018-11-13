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
        public int MetronomeSpeed { get; set; }
        public int Seconds { get; set; } // this will be calculated by recording a list of small recording periods.

        // You still need these to work out your metronome calculations.
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int ExerciseId { get; set; }
   
    }
}
