using CygSoft.SmartSession.GoalManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.GoalManagement
{
    public abstract class GoalTask : IGoalTask
    {
        protected List<SessionResult> results;

        public int MinutesPracticed
        {
            get
            {
                if (results == null)
                    return 0;
                if (results.Count == 0)
                    return 0;

                return results.Sum(r => r.Minutes);
            }
        }

        public double Weighting { get; internal set; }

        public DateTime CreateDate { get; private set; }

        // inferred by whatever the first session result is...
        public DateTime? StartDate
        {
            get
            {
                if (results == null)
                    return null;
                if (results.Count == 0)
                    return null;
                return results.Min(r => r.StartTime);
            }
        }

        public GoalTask(string title)
        {
            Id = Guid.NewGuid().ToString();
            CreateDate = DateTime.Now;
            Title = title;
            this.results = null;
        }

        public GoalTask(string title, DateTime createDate, List<SessionResult> results)
        {
            Id = Guid.NewGuid().ToString();
            CreateDate = createDate;
            Title = title;
            this.results = results;
        }

        public abstract double PercentCompleted { get; }

        public string Title { get; private set; }

        public string Id { get; private set; }
    }
}
