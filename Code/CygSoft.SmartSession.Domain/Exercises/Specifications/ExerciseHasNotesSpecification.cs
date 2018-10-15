using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Exercises.Specifications
{
    public class ExerciseHasNotesSpecification : Specification<Exercise>
    {
        private readonly bool? hasNotes;

        public ExerciseHasNotesSpecification(bool? hasNotes)
        {
            this.hasNotes = hasNotes;
        }

        public override Expression<Func<Exercise, bool>> ToExpression()
        {
            if (hasNotes == null)
                return ex => true;

            else
                return ex => string.IsNullOrWhiteSpace(ex.Notes) == !hasNotes;
        }
    }
}
