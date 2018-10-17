using CygSoft.SmartSession.Domain.Common;
using System;
using System.Linq.Expressions;

namespace CygSoft.SmartSession.Domain.Exercises.Specifications
{
    public class ExerciseTargetMetronomeSpeedSpecification : Specification<Exercise>
    {
        private int? targetMetronomeSpeed;
        private ComparisonOperators comparison;

        public ExerciseTargetMetronomeSpeedSpecification(int? targetMetronomeSpeed, ComparisonOperators comparison)
        {
            this.targetMetronomeSpeed = targetMetronomeSpeed;
            this.comparison = comparison;
        }

        public override Expression<Func<Exercise, bool>> ToExpression()
        {
            if (!targetMetronomeSpeed.HasValue)
                return ex => true;

            switch (comparison)
            {
                case ComparisonOperators.LessThan:
                    return ex => ex.TargetMetronomeSpeed < targetMetronomeSpeed.Value;

                case ComparisonOperators.LessThanOrEqualTo:
                    return ex => ex.TargetMetronomeSpeed <= targetMetronomeSpeed.Value;

                case ComparisonOperators.GreaterThan:
                    return ex => ex.TargetMetronomeSpeed > targetMetronomeSpeed.Value;

                case ComparisonOperators.GreaterThanOrEqualTo:
                    return ex => ex.TargetMetronomeSpeed >= targetMetronomeSpeed.Value;

                default:
                    return ex => false;
            }
        }
    }
}
