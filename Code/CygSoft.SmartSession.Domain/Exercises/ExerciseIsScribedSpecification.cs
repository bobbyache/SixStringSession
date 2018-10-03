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
        private readonly bool? isScribed;

        public ExerciseIsScribedSpecification(bool? isScribed)
        {
            this.isScribed = isScribed;
        }

        public override Expression<Func<Exercise, bool>> ToExpression()
        {
            if (isScribed == null)
                return ex => true;

            else
                return ex => ex.Scribed == isScribed;
        }
    }
}
