using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Goals;
using CygSoft.SmartSession.Domain.Keywords;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.EF.Repositories
{
    public class GoalRepository : BaseRepository<Goal>, IGoalRepository
    {
        public GoalRepository(SmartSessionContext context) : base(context) { }

        public override IReadOnlyList<Goal> Find(Specification<Goal> specification, int page = 0, int pageSize = 100)
        {
            var exs = context.Goals
                //.Include(ex => ex.GoalKeywords)
                //    .ThenInclude(keyword => keyword.Keyword)
                .Where(specification.ToExpression())

            //    .Skip(page * pageSize)
            //    .Take(pageSize)
            //    .ToList();
            ;

            return exs.ToList();
        }

        public IReadOnlyList<Goal> Find(Specification<Goal> specification, string[] keywords, int page = 0, int pageSize = 100)
        {


            var exs = context.Goals
                //.Include(ex => ex.GoalKeywords)
                //.ThenInclude(keyword => keyword.Keyword)
                .Where(specification.ToExpression())

                .Where(goal => goal.GoalKeywords
                    .Select(exKey => exKey.Keyword.Word)
                    //.Where(w => w == "Vibrato")
                    .Where(w => keywords.Contains(w))
                    .Any()
                    )
            ;

            return exs.ToList();
        }
    }
}