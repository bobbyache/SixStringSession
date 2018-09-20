using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Common
{
    public class TriStateCheck
    {
        public bool IsSatisfiedBy(bool value, bool? constraint)
        {
            if (constraint == null)
                return true;

            else
                return constraint.Value == value;
        }
    }
}
