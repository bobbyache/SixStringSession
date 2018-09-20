using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public class ExerciseHasNotesSpecification : Specification<Exercise>
    {
        private readonly bool? hasNotes;

        public ExerciseHasNotesSpecification(bool? isScribed)
        {
            this.hasNotes = isScribed;
        }

        public override Expression<Func<Exercise, bool>> ToExpression()
        {
            
            return exercise => new TriStateCheck().IsSatisfiedBy(!string.IsNullOrWhiteSpace(exercise.Notes), hasNotes);
        }
    }
}
