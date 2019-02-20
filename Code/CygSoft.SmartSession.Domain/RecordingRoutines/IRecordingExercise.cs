using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.RecordingRoutines
{
    public interface IRecordingExercise
    {
        string Title { get; }

        int? CurrentSpeed { get; }
        int CurrentTotalSeconds { get; }

        double CurrentManualProgress { get; }
        double CurrentTimeProgress { get; }
        double CurrentSpeedProgress { get; }
        double CurrentOverAllProgress { get; }

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
    }
}
