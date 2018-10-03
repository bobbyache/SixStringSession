using CygSoft.SmartSession.Domain.Common;
using System;
using System.Linq.Expressions;

namespace CygSoft.SmartSession.Domain.Exercises.Specifications
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
            if (!practicalityRating.HasValue)
                return ex => true;

            switch (comparison)
            {
                case ComparisonOperators.LessThan:
                    return ex => ex.PracticalityRating < practicalityRating.Value;

                case ComparisonOperators.LessThanOrEqualTo:
                    return ex => ex.PracticalityRating <= practicalityRating.Value;

                case ComparisonOperators.GreaterThan:
                    return ex => ex.PracticalityRating > practicalityRating.Value;

                case ComparisonOperators.GreaterThanOrEqualTo:
                    return ex => ex.PracticalityRating >= practicalityRating.Value;

                default:
                    return ex => false;
            }
        }
    }
}
