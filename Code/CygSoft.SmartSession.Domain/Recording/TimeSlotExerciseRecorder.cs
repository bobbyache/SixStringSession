using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Recording
{
    public class TimeSlotExerciseRecorder : ExerciseRecorder, ITimeSlotExerciseRecorder
    {
        public TimeSlotExerciseRecorder(IRecorder recorder, int exerciseId, string title,
            ISpeedProgress speedProgress, IPracticeTimeProgress practiceTimeProgress, IManualProgress manualProgress, int assignedTime)
            : base(recorder, exerciseId, title, speedProgress, practiceTimeProgress, manualProgress)
        {
            AssignedSeconds = assignedTime;
        }

        protected override void TickEventFired(object sender, EventArgs e)
        {
            base.TickEventFired(sender, e);
        }

        public double AssignedSeconds { get; private set; }
        public double RemainingSeconds
        {
            get
            {
                var result = AssignedSeconds - RecordedSeconds;
                if (result < 0)
                    return 0;
                return AssignedSeconds - RecordedSeconds;
            }
        }
    }
}
