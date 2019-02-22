using CygSoft.SmartSession.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Recording
{
  public class PracticeRoutineRecorder
  {
        IEnumerable<IExerciseRecorder> exerciseRecorders;

        public string Title { get; private set; }
        public int Id { get; private set; }

        public double RecordedSeconds { get => exerciseRecorders.Sum(ex => ex.RecordedSeconds); }
        public string RecordedSecondsDisplay { get => TimeFuncs.DisplayTimeFromSeconds(RecordedSeconds); }

        public PracticeRoutineRecorder(string title, IEnumerable<IExerciseRecorder> exerciseRecorders) 
            : this(0, title, exerciseRecorders)
        {
        }

        public PracticeRoutineRecorder(int id, string title, IEnumerable<IExerciseRecorder> exerciseRecorders)
        {
            Id = id;
            Title = title;
            this.exerciseRecorders = exerciseRecorders ?? throw new ArgumentNullException("Recording exercises are mandatory.");
        }
    }
}
