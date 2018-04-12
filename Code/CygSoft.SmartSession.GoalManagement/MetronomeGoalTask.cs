using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement
{
    public class MetronomeGoalTask : GoalTask
    {
        public int CurrentSpeed => 0;
        public int TargetSpeed => 0;

        public override int PercentCompleted => 0;
        //{
        //    get
        //    {
        //        // The idea is that if one starts off with a metronome speed of 50, but your 
        //        // target metronome speed is 90, and your currently checked off speed is 70, then your percentage will be...
        //        // ((90 - 70) / (90 - 50)) * 100 ...
        //        throw new NotImplementedException();
        //    }
        //}

    }
}
