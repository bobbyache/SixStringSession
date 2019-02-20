﻿using System;

namespace CygSoft.SmartSession.Domain.Sessions
{
    public interface IRecorder
    {
        DateTime? EndTime { get; }
        bool Recording { get; }
        double PreciseSeconds { get; }
        int Seconds { get; }
        DateTime? StartTime { get; }
        Action TickActionCallBack { set; }

        string DisplayTime { get; }

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