using CygSoft.SmartSession.Desktop.Exercises.Recorder;
using CygSoft.SmartSession.Domain.Recording;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Supports.DI
{
    public interface IViewModelFactory
    {
        ExerciseRecorderViewModel CreateRecorderViewModel(IExerciseRecorder exerciseRecorder);
        void Release(ExerciseRecorderViewModel recorderViewModel);

        //RecorderViewModel CreateRecorderViewModel(IRecorder recorder, int exerciseId, string title,
        //    ISpeedProgress speedProgress, IPracticeTimeProgress practiceTimeProgress, IManualProgress manualProgress);

        //IRecorder CreateRecorder();
        //ISpeedProgress CreateSpeedProgress(int initialSpeed, int currentSpeed, int targetSpeed, int weighting);
        //IManualProgress CreateManualProgress(int value, int weighting);
        //IPracticeTimeProgress CreatePracticeTimeProgress(int currentTime, int targetTime, int weighting);

        //void Release(IManualProgress manuaProgress);
        //void Release(ISpeedProgress manuaProgress);
        //void Release(IPracticeTimeProgress manuaProgress);
        //void Release(IRecorder recorder);
    }
}
