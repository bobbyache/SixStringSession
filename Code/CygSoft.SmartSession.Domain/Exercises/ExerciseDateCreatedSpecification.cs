using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public class ExerciseDateCreatedSpecification : Specification<Exercise>
    {
        private readonly DateTime? startDate;
        private readonly DateTime? endDate;

        public ExerciseDateCreatedSpecification(DateTime? startDate, DateTime? endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public override Expression<Func<Exercise, bool>> ToExpression()
        {
            return exercise => new DateRangeCheck().IsSatisfiedBy(exercise.DateCreated, startDate, endDate);
        }
    }
}
