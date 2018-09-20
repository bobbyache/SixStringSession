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
            return exercise => new ComparisonCheck()
                .IsSatisfiedBy(optimalDuration, exercise.OptimalDuration, comparison);
        }
    }
}
