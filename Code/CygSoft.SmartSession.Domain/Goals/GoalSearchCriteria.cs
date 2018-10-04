using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Goals.Specifications;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Goals
{
    public class GoalSearchCriteria : IGoalSearchCriteria
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

        public Specification<Goal> Specification()
        {
            return
                new GoalTitleSpecification(Title)
                .And(new GoalDateModifiedSpecification(DateModifiedAfter, DateModifiedBefore))
                .And(new GoalDateCreatedSpecification(DateCreatedAfter, DateCreatedBefore))
                .And(new GoalHasNotesSpecification(HasNotes))
            ;
        }
    }
}