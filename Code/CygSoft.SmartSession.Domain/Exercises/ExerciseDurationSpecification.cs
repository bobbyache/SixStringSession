using CygSoft.SmartSession.Domain.Common;
using System;
using System.Linq.Expressions;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public class ExerciseDurationSpecification : Specification<Exercise>
    {
        private readonly int? optimalDuration;
        private readonly ComparisonOperators comparison;

        public ExerciseDurationSpecification(int? optimalDuration, ComparisonOperators comparison)
        {
            this.optimalDuration = optimalDuration;
            this.comparison = comparison;
        }

        public override Expression<Func<Exercise, bool>> ToExpression()
        {
            if (!optimalDuration.HasValue)
                return ex => true;

            switch (comparison)
            {
                case ComparisonOperators.LessThan:
                    return ex => ex.OptimalDuration < optimalDuration.Value;

                case ComparisonOperators.LessThanOrEqualTo:
                    return ex => ex.OptimalDuration <= optimalDuration.Value;

                case ComparisonOperators.GreaterThan:
                    return ex => ex.OptimalDuration > optimalDuration.Value;

                case ComparisonOperators.GreaterThanOrEqualTo:
                    return ex=> ex.OptimalDuration >= optimalDuration.Value;

                default:
                    return ex => false;
            }
        }
    }
}
