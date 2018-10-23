using System;

namespace CygSoft.SmartSession.Domain.Sessions
{
    public interface IExerciseRecorder
    {
        DateTime EndTime { get; }
        bool Recording { get; }
        double Seconds { get; }
        DateTime StartTime { get; }
        Action TickActionCallBack { set; }

        event EventHandler RecordingStatusChanged;

        void Clear();
        void Pause();
        void Start();
    }
}