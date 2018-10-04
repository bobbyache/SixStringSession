using CygSoft.SmartSession.Domain.Exercises;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.DomainLegacy
{
    public class PracticeTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public int GoalTaskType { get; set; }

        public int TargetSpeed { get; set; }
        public int TargetPracticeDuration { get; set; }        

        public Exercise Exercise { get; set; }

        public virtual List<SessionPracticeTask> TaskSessions { get; set; }
    }
}
