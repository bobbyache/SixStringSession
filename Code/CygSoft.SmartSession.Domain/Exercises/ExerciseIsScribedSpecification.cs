using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public class ExerciseIsScribedSpecification : Specification<Exercise>
    {
        public override Expression<Func<Exercise, bool>> ToExpression()
        {
            throw new NotImplementedException();
        }
    }
}
