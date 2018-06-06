﻿using CygSoft.SmartSession.GoalManagement.Infrastructure;
using System;

namespace CygSoft.SmartSession.GoalManagement.Sessions
{
    public abstract class SessionResult : IdentityItem
    {
        private DateTime startTime;
        private DateTime endTime;

        public SessionResult(DateTime startTime, DateTime endTime)
        {
            if (startTime > endTime)
                throw new ArgumentOutOfRangeException("Session minutes cannot be a negative value.");

            this.startTime = startTime;
            this.endTime = endTime;
        }

        public SessionResult(string id, DateTime startTime, DateTime endTime) : base(id)
        {
            if (startTime > endTime)
                throw new ArgumentOutOfRangeException("Session minutes cannot be a negative value.");

            this.startTime = startTime;
            this.endTime = endTime;
        }

        public int Minutes { get { return (int)Math.Round(endTime.Subtract(startTime).TotalMinutes, 0); } }
        public DateTime StartTime { get { return this.startTime; } }
    }
}
