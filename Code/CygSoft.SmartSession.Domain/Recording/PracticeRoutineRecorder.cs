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
        IEnumerable<IExerciseRecorder> recordingExercises;

        public string Title { get; private set; }
        public int Id { get; private set; }

        public double RecordedSeconds { get => recordingExercises.Sum(ex => ex.RecordedSeconds); }
        public string RecordedSecondsDisplay { get => TimeFuncs.DisplayTimeFromSeconds(RecordedSeconds); }

        public PracticeRoutineRecorder(string title, IEnumerable<IExerciseRecorder> recordingExercises) 
            : this(0, title, recordingExercises)
        {
        }

        public PracticeRoutineRecorder(int id, string title, IEnumerable<IExerciseRecorder> recordingExercises)
        {
            Id = id;
            Title = title;
            this.recordingExercises = recordingExercises ?? throw new ArgumentNullException("Recording exercises are mandatory.");
        }
    }
}
