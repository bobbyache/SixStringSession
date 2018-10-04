using CygSoft.SmartSession.Domain.Common;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Goals
{
    public interface IGoalRepository : IRepository<Goal>
    {
        // Only add really custom stuff here like...
        // GetGoalsWhere...
        IReadOnlyList<Goal> Find(Specification<Goal> specification, string[] keywords, int page = 0, int pageSize = 100);
    }
}
