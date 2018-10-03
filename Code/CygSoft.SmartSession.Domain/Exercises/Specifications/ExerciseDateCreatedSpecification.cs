using CygSoft.SmartSession.Domain.Common;
using System;
using System.Linq.Expressions;

namespace CygSoft.SmartSession.Domain.Exercises.Specifications
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
            if (startDate.HasValue && endDate.HasValue)
            {
                return ex => ex.DateCreated >= startDate && ex.DateCreated <= endDate;
            }
            else if (!startDate.HasValue && !endDate.HasValue)
            {
                return ex => true;

            }
            else if (startDate.HasValue)
            {
                return ex => ex.DateCreated >= startDate;
            }
            else if (endDate.HasValue)
            {
                return ex => ex.DateCreated <= endDate;
            }
            else
            {
                return ex => false;
            }
        }
    }
}
