using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises.Specifications;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public class ExerciseSearchCriteria : IExerciseSearchCriteria
    {
        public DateTime? DateCreatedAfter { get; set; }
        public DateTime? DateCreatedBefore { get; set; }
        public DateTime? DateModifiedAfter { get; set; }
        public DateTime? DateModifiedBefore { get; set; }
        public int? DifficultyRating { get; set; }
        public ComparisonOperators DifficultyRatingOperator { get; set; }
        public ComparisonOperators OptimalDurationOperator { get; set; }
        public ComparisonOperators PracticalityRatingOperator { get; set; }
        public int? OptimalDuration { get; set; }
        public bool? HasNotes { get; set; }
        public int? PracticalityRating { get; set; }
        public string Title { get; set; }
        public string Keywords { get; set; }

        public string[] KeywordSpecification()
        {
            return Keywords.Split(new char[] { ',' });
        }

        public Specification<Exercise> Specification()
        {
            return
                new ExerciseTitleSpecification(Title)
                .And(new ExerciseDateModifiedSpecification(DateModifiedAfter, DateModifiedBefore))
                .And(new ExerciseDateCreatedSpecification(DateCreatedAfter, DateCreatedBefore))
                .And(new ExerciseDurationSpecification(OptimalDuration, OptimalDurationOperator))
                .And(new ExerciseDifficultyRatingSpecification(DifficultyRating, DifficultyRatingOperator))
                .And(new ExercisePracticalityRatingSpecification(PracticalityRating, PracticalityRatingOperator))
                .And(new ExerciseHasNotesSpecification(HasNotes))
            ;
        }
    }
}
