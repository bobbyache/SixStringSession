using CygSoft.SmartSession.Domain.Sessions;
using CygSoft.SmartSession.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.Domain.Tasks
{
    public abstract class GoalTask<T>
        : EntityBase, IEditableGoalTask, IWeightedEntity
        where T : SessionResult
    {
        protected List<T> sessionResultList;

        public event EventHandler WeightingChanged;

        public int MinutesPracticed
        {
            get
            {
                if (sessionResultList == null)
                    return 0;
                if (sessionResultList.Count == 0)
                    return 0;

                return sessionResultList.Sum(r => r.Minutes);
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
                if (sessionResultList == null)
                    return null;
                if (sessionResultList.Count == 0)
                    return null;
                return sessionResultList.Min(r => r.StartTime);
            }
        }

        public GoalTask()
        {
            CreateDate = DateTime.Now;
            this.sessionResultList = new List<T>();
        }

        public abstract double PercentCompleted { get; }

        public string Title { get; set; }

        internal void AddSession(T sessionResult)
        {
            sessionResultList.Add(sessionResult);
        }

        internal void AddSessionRange(IEnumerable<T> sessionResults)
        {
            sessionResultList.AddRange(sessionResults);
        }
    }
}
