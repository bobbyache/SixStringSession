using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Exercises.Specifications
{
    public class ExerciseTargetPracticeTimeSpecification : Specification<Exercise>
    {
        private int? targetPracticeTime;
        private ComparisonOperators comparison;

        public ExerciseTargetPracticeTimeSpecification(int? targetPracticeTime, ComparisonOperators comparison)
        {
            this.targetPracticeTime = targetPracticeTime;
            this.comparison = comparison;
        }

        public override Expression<Func<Exercise, bool>> ToExpression()
        {
            if (!targetPracticeTime.HasValue)
                return ex => true;

            switch (comparison)
            {
                case ComparisonOperators.LessThan:
                    return ex => ex.TargetPracticeTime.Value < targetPracticeTime.Value;

                case ComparisonOperators.LessThanOrEqualTo:
                    return ex => ex.TargetPracticeTime.Value <= targetPracticeTime.Value;

                case ComparisonOperators.GreaterThan:
                    return ex => ex.TargetPracticeTime.Value > targetPracticeTime.Value;

                case ComparisonOperators.GreaterThanOrEqualTo:
                    return ex => ex.TargetPracticeTime.Value >= targetPracticeTime.Value;

                default:
                    return ex => false;
            }
        }
    }
}
