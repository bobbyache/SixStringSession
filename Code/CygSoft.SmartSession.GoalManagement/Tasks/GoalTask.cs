using CygSoft.SmartSession.GoalManagement.Infrastructure;
using CygSoft.SmartSession.GoalManagement.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.GoalManagement.Tasks
{
    public abstract class GoalTask : IdentityItem, IGoalTask
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

        public GoalTask(string title) : base()
        {
            CreateDate = DateTime.Now;
            Title = title;
            this.sessionResults = new List<SessionResult>();
        }

        public GoalTask(string id, string title, DateTime createDate, List<SessionResult> results) : base(id)
        {
            CreateDate = createDate;
            Title = title;
            this.sessionResults = results;
        }

        public abstract double PercentCompleted { get; }

        public string Title { get; private set; }

        internal void AddSession(SessionResult sessionResult)
        {
            sessionResults.Add(sessionResult);
        }
    }
}
