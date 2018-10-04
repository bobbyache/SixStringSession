
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Goals
{
    public interface IGoalService
    {
        Goal Get(int id);
        IEnumerable<Goal> Find(GoalSearchCriteria searchCriteria);
        void Remove(int id);
        void Add(Goal goal);
        void Update(Goal goal);
    }
}
