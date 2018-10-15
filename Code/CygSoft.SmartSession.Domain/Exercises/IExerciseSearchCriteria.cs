using System;
using CygSoft.SmartSession.Domain.Common;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public interface IExerciseSearchCriteria
    {
        DateTime? DateCreatedAfter { get; set; }
        DateTime? DateCreatedBefore { get; set; }
        DateTime? DateModifiedAfter { get; set; }
        DateTime? DateModifiedBefore { get; set; }
        int? DifficultyRating { get; set; }
        ComparisonOperators DifficultyRatingOperator { get; set; }
        ComparisonOperators OptimalDurationOperator { get; set; }
        ComparisonOperators PracticalityRatingOperator { get; set; }
        int? OptimalDuration { get; set; }
        bool? HasNotes { get; set; }
        int? PracticalityRating { get; set; }
        string Title { get; set; }

        string Keywords { get; set; }
    }
}