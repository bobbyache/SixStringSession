using CygSoft.SmartSession.Domain.Sessions;
using CygSoft.SmartSession.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.Domain.Tasks
{
    public abstract class GoalTask : IGoalTaskRecord, IEditableGoalTask
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

        public DateTime CreateDate { get; set; }

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

        public GoalTask()
        {
            InstanceId = Guid.NewGuid().ToString();
            CreateDate = DateTime.Now;
            this.sessionResults = new List<SessionResult>();
        }

        public abstract double PercentCompleted { get; }

        public string Title { get; set; }

        public int Id { get; set; }

        public string InstanceId { get; }

        internal void AddSession(SessionResult sessionResult)
        {
            sessionResults.Add(sessionResult);
        }
    }
}
