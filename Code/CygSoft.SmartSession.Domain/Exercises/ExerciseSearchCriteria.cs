using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises.Specifications;
using System;

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
        public bool? IsScribed { get; set; }
        public int? PracticalityRating { get; set; }
        public string Title { get; set; }

        public Specification<Exercise> Specification()
        {
            return
                new ExerciseTitleSpecification(Title)
                .And(new ExerciseDateModifiedSpecification(DateModifiedAfter, DateModifiedBefore))
                .And(new ExerciseDateCreatedSpecification(DateCreatedAfter, DateCreatedBefore))
                .And(new ExerciseDurationSpecification(OptimalDuration, OptimalDurationOperator))
                .And(new ExerciseDifficultyRatingSpecification(DifficultyRating, DifficultyRatingOperator))
                .And(new ExercisePracticalityRatingSpecification(PracticalityRating, PracticalityRatingOperator))
                .And(new ExerciseIsScribedSpecification(IsScribed))
                .And(new ExerciseHasNotesSpecification(HasNotes))
                //.And(new ExerciseKeywordsSpecification(new List<string> { "Technique" }))
            ;
        }
    }
}
