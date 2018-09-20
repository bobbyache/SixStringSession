using CygSoft.SmartSession.Domain.Common;
using System;
using System.Linq.Expressions;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public class ExerciseDateModifiedSpecification : Specification<Exercise>
    {
        private readonly DateTime? startDate;
        private readonly DateTime? endDate;

        public ExerciseDateModifiedSpecification(DateTime? startDate, DateTime? endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public override Expression<Func<Exercise, bool>> ToExpression()
        {
            return exercise => new DateRangeCheck().IsSatisfiedBy(exercise.DateModified, startDate, endDate);
        }
    }
}
