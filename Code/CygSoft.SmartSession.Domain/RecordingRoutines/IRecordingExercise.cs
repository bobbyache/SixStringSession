using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.RecordingRoutines
{
    public interface IRecordingExercise
    {
        bool Recording { get; }
        double RecordedSeconds { get; }
        Action TickActionCallBack { set; }
        string RecordedSecondsDisplay { get; }

        event EventHandler RecordingStatusChanged;

        void Reset();
        void Pause();
        void Resume();

        void AddSeconds(int seconds);
        void SubstractSeconds(int seconds);
        void AddMinutes(int minutes);
        void SubtractMinutes(int minutes);

        void Dispose();
    }
}
