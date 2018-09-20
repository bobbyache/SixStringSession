using CygSoft.SmartSession.Domain.Common;
using System;
using System.Linq.Expressions;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public class ExercisePracticalityRatingSpecification : Specification<Exercise>
    {
        private int? practicalityRating;
        private ComparisonOperators comparison;

        public ExercisePracticalityRatingSpecification(int? practicalityRating, ComparisonOperators comparison)
        {
            this.practicalityRating = practicalityRating;
            this.comparison = comparison;
        }

        public override Expression<Func<Exercise, bool>> ToExpression()
        {
            return exercise => new ComparisonCheck()
                .IsSatisfiedBy(practicalityRating, exercise.PracticalityRating, comparison);
        }
    }
}
