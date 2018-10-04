using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.GoalTasks;
using CygSoft.SmartSession.Domain.Keywords;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.EF.Repositories
{
    public class GoalTaskRepository : BaseRepository<GoalTask>, IGoalTaskRepository
    {
        public GoalTaskRepository(SmartSessionContext context) : base(context) { }

        public override IReadOnlyList<GoalTask> Find(Specification<GoalTask> specification, int page = 0, int pageSize = 100)
        {
            var exs = context.GoalTasks
                //.Include(ex => ex.GoalTaskKeywords)
                //    .ThenInclude(keyword => keyword.Keyword)
                .Where(specification.ToExpression())

            //    .Skip(page * pageSize)
            //    .Take(pageSize)
            //    .ToList();
            ;

            return exs.ToList();
        }

        public IReadOnlyList<GoalTask> Find(Specification<GoalTask> specification, string[] keywords, int page = 0, int pageSize = 100)
        {


            var exs = context.GoalTasks
                //.Include(ex => ex.GoalTaskKeywords)
                //.ThenInclude(keyword => keyword.Keyword)
                .Where(specification.ToExpression())

                .Where(goalTask => goalTask.GoalTaskKeywords
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