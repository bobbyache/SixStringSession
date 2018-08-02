using CygSoft.SmartSession.Domain.Sessions;
using CygSoft.SmartSession.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.Domain.Tasks
{
    public abstract class GoalTask : IGoalTask
    {
        protected List<SessionResult> sessionResults;

        public event EventHandler WeightingChanged;

        public int MinutesPracticed
        {
            get
            {
                if (sessionResults == null)
                    return 0;
                if (sessionResults.Count == 0)
                    return 0;

                return sessionResults.Sum(r => r.Minutes);
            }
        }

        private int weighting;
        public int Weighting
        {
            get { return this.weighting; }
            set
            {
                if (this.weighting != value)
                {
                    this.weighting = value;
                    WeightingChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        public DateTime CreateDate { get; private set; }

        // inferred by whatever the first session result is...
        public DateTime? StartDate
        {
            get
            {
                if (sessionResults == null)
                    return null;
                if (sessionResults.Count == 0)
                    return null;
                return sessionResults.Min(r => r.StartTime);
            }
        }

        public GoalTask(string title)
        {
            Id = Guid.NewGuid().ToString();
            CreateDate = DateTime.Now;
            Title = title;
            this.sessionResults = new List<SessionResult>();
        }

        public GoalTask(string title, DateTime createDate, List<SessionResult> results)
        {
            Id = Guid.NewGuid().ToString();
            CreateDate = createDate;
            Title = title;
            this.sessionResults = results;
        }

        public abstract double PercentCompleted { get; }

        public string Title { get; private set; }

        public string Id { get; private set; }

        internal void AddSession(SessionResult sessionResult)
        {
            sessionResults.Add(sessionResult);
        }
    }
}
