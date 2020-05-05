using CygSoft.SmartSession.Domain.Exercises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Recording
{
    public interface IExerciseRecorder
    {
        int ExerciseId { get; }
        string Title { get; }

        int CurrentSpeed { get; set; }
        int TargetSpeed { get; }
        int CurrentTotalSeconds { get; }

        int CurrentManualProgress { get; set; }
        int CurrentTimeProgress { get; }
        int CurrentSpeedProgress { get; }
        int CurrentOverAllProgress { get; }

        bool Recording { get; }
        double RecordedSeconds { get; }
        Action TickActionCallBack { set; }

        string RecordedSecondsDisplay { get; }
        string TotalSecondsDisplay { get; }

        event EventHandler RecordingStatusChanged;

        void Reset();
        void Pause();
        void Resume();

        void IncrementSpeed(int ticks);
        void DecrementSpeed(int ticks);

        void IncrementManualProgress(int value);
        void DecrementManualProgress(int value);

        void AddSeconds(int seconds);
        void SubstractSeconds(int seconds);
        void AddMinutes(int minutes);
        void SubtractMinutes(int minutes);

        void Dispose();

        void SaveRecording(IExerciseService exerciseService);

        bool ProgressBySpeed { get; }
        bool ProgressByTime { get; }
        bool ProgressByManualInput { get; }

        double SpeedProgressPercentageAllocation { get; }
        double TimeProgressPercentageAllocation { get; }
        double ManualProgressPercentageAllocation { get; }
    }
}
