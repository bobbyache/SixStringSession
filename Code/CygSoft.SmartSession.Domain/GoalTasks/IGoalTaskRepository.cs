using CygSoft.SmartSession.Domain.Common;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.GoalTasks
{
    public interface IGoalTaskRepository : IRepository<GoalTask>
    {
        // Only add really custom stuff here like...
        // GetGoalTasksWhere...
        IReadOnlyList<GoalTask> Find(Specification<GoalTask> specification, string[] keywords, int page = 0, int pageSize = 100);
    }
}
