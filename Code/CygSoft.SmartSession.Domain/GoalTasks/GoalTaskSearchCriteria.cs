using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.GoalTasks.Specifications;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.GoalTasks
{
    public class GoalTaskSearchCriteria : IGoalTaskSearchCriteria
    {
        public DateTime? DateCreatedAfter { get; set; }
        public DateTime? DateCreatedBefore { get; set; }
        public DateTime? DateModifiedAfter { get; set; }
        public DateTime? DateModifiedBefore { get; set; }
        public bool? HasNotes { get; set; }
        public string Title { get; set; }
        public string Keywords { get; set; }

        public string[] KeywordSpecification()
        {
            return Keywords.Split(new char[] { ',' });
        }

        public Specification<GoalTask> Specification()
        {
            return
                new GoalTaskTitleSpecification(Title)
                .And(new GoalTaskDateModifiedSpecification(DateModifiedAfter, DateModifiedBefore))
                .And(new GoalTaskDateCreatedSpecification(DateCreatedAfter, DateCreatedBefore))
                .And(new GoalTaskHasNotesSpecification(HasNotes))
            ;
        }
    }
}
