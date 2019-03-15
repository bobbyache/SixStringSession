using CygSoft.SmartSession.Desktop.Exercises.Recorder;
using CygSoft.SmartSession.Domain.Recording;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Supports.Factories
{
    public interface IViewModelFactory
    {
        ExerciseRecorderViewModel CreateRecorderViewModel(IExerciseRecorder exerciseRecorder);
    }
}
