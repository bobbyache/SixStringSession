using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Recording
{
    public class TimeSlotExerciseRecorder : ExerciseRecorder
    {
        public TimeSlotExerciseRecorder(IRecorder recorder, int exerciseId, string title,
            ISpeedProgress speedProgress, IPracticeTimeProgress practiceTimeProgress, IManualProgress manualProgress, int assignedTime)
            : base(recorder, exerciseId, title, speedProgress, practiceTimeProgress, manualProgress)
        {
            AssignedTime = assignedTime;
        }

        protected override void TickEventFired(object sender, EventArgs e)
        {
            base.TickEventFired(sender, e);
        }

        public double AssignedTime { get; private set; }
        public double RemainingTime
        {
            get
            {
                var result = AssignedTime - RecordedSeconds;
                if (result < 0)
                    return 0;
                return AssignedTime - RecordedSeconds;
            }
        }
    }
}
