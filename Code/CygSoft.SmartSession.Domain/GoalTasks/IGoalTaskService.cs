
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.GoalTasks
{
    public interface IGoalTaskService
    {
        GoalTask Get(int id);
        IEnumerable<GoalTask> Find(GoalTaskSearchCriteria searchCriteria);
        void Remove(int id);
        void Add(GoalTask goalTask);
        void Update(GoalTask goalTask);
    }
}
