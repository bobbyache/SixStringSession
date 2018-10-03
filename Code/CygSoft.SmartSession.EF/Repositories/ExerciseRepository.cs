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
                .Include(ex => ex.ExerciseKeywords)
                    .ThenInclude(post => post.Keyword)
                .Where(specification.ToExpression())

                    //    .Skip(page * pageSize)
                    //    .Take(pageSize)
                    //    .ToList();
            ;

            return exs.ToList();
        }
    }
}
