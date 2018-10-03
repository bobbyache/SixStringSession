using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public class ExerciseDifficultyRatingSpecification : Specification<Exercise>
    {
        private int? difficultyRating;
        private ComparisonOperators comparison;

        public ExerciseDifficultyRatingSpecification(int? difficultyRating, ComparisonOperators comparison)
        {
            this.difficultyRating = difficultyRating;
            this.comparison = comparison;
        }

        public override Expression<Func<Exercise, bool>> ToExpression()
        {
            if (!difficultyRating.HasValue)
                return ex => true;

            switch (comparison)
            {
                case ComparisonOperators.LessThan:
                    return ex => ex.DifficultyRating < difficultyRating.Value;

                case ComparisonOperators.LessThanOrEqualTo:
                    return ex => ex.DifficultyRating <= difficultyRating.Value;

                case ComparisonOperators.GreaterThan:
                    return ex => ex.DifficultyRating > difficultyRating.Value;

                case ComparisonOperators.GreaterThanOrEqualTo:
                    return ex => ex.DifficultyRating >= difficultyRating.Value;

                default:
                    return ex => false;
            }

        }
    }
}
