using CygSoft.SmartSession.Domain.Sessions;
using CygSoft.SmartSession.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.Domain.Tasks
{
    public abstract class GoalTask<T>
        : BaseTask
        where T : SessionResult
    {
        protected List<T> sessionResultList;

        public override int MinutesPracticed
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

        public override DateTime? StartDate
        {
            get
            {
                // inferred by whatever the first session result is...
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
