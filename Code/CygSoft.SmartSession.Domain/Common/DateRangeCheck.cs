using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Common
{
    public class DateRangeCheck
    {
        public bool IsSatisfiedBy(DateTime date, DateTime? startDate, DateTime? endDate)
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
