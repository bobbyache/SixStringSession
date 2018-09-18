using System;
using System.Linq.Expressions;

namespace CygSoft.SmartSession.Domain.Common
{
    public class DateCreatedRangeSpecification : Specification<Entity>
    {
        private readonly DateTime? startDate;
        private readonly DateTime? endDate;

        public DateCreatedRangeSpecification(DateTime? startDate, DateTime? endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public override Expression<Func<Entity, bool>> ToExpression()
        {
            return entity => WithinRange(entity.DateCreated);
        }

        private bool WithinRange(DateTime date)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                if (date >= startDate && date <= endDate)
                    return true;
            }
            else if (!startDate.HasValue && !endDate.HasValue)
            {
                return true;
            }
            else if (startDate.HasValue)
            {
                return date >= startDate;
            }
            else if (endDate.HasValue)
            {
                return date <= endDate;
            }

            return false;
        }
    }
}
