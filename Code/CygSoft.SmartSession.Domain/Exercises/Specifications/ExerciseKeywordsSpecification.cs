using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Exercises.Specifications
{
    public class ExerciseKeywordsSpecification : Specification<Exercise>
    {
        List<string> keywords;

        public ExerciseKeywordsSpecification(List<string> keywords)
        {
            this.keywords = keywords.Select(k => k.ToUpper()).ToList();
        }

        public override Expression<Func<Exercise, bool>> ToExpression()
        {
            return exercise => exercise.ExerciseKeywords.Select(ex => ex.Keyword.Word).Except(keywords).Any();
        }
    }
}
