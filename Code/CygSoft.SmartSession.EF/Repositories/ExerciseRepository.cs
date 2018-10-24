using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Keywords;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.EF.Repositories
{
    public class ExerciseRepository : BaseRepository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(SmartSessionContext context) : base(context) { }

        public override IReadOnlyList<Exercise> Find(Specification<Exercise> specification, int page = 0, int pageSize = 100)
        {
            var exs = context.Exercises
                .Include(ex => ex.ExerciseActivity)
                .Where(specification.ToExpression())
            //    .Skip(page * pageSize)
            //    .Take(pageSize)
            //    .ToList();
            ;

            return exs.ToList();
        }

        public IReadOnlyList<Exercise> Find(Specification<Exercise> specification, string[] keywords, int page = 0, int pageSize = 100)
        {
            var exs = context.Exercises
                .Include(ex => ex.ExerciseActivity)
                .Where(specification.ToExpression())
                .Where(exercise => !keywords
                    .Except(exercise.ExerciseKeywords.Select(exKey => exKey.Keyword.Word.ToUpper()))
                    .Any()
                )
            ;

            return exs.ToList();
        }
    }
}
