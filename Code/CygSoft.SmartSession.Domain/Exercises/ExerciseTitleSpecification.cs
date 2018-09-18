using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public class ExerciseTitleSpecification : Specification<Exercise>
    {
        private readonly string titleFragment;

        public ExerciseTitleSpecification(string titleFragment)
        {
            this.titleFragment = titleFragment;
        }

        public override Expression<Func<Exercise, bool>> ToExpression()
        {
            return exercise => ContainsText(exercise, titleFragment);
        }

        private bool ContainsText(Exercise exercise, string titleFragment)
        {
            if (string.IsNullOrEmpty(titleFragment))
                return true;

            return exercise.Title.ToUpper().Contains(titleFragment.ToUpper());
        }
    }
}
