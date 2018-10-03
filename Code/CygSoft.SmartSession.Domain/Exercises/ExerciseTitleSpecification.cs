using CygSoft.SmartSession.Domain.Common;
using System;
using System.Linq.Expressions;

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
            if (string.IsNullOrEmpty(titleFragment))
                return ex => true;
            else
                return ex => ex.Title.ToUpper().Contains(titleFragment.ToUpper());
        }

        private bool ContainsText(Exercise exercise, string titleFragment)
        {
            if (string.IsNullOrEmpty(titleFragment))
                return true;

            return exercise.Title.ToUpper().Contains(titleFragment.ToUpper());
        }
    }
}
