using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Goals
{
    public interface IGoalRepository
    {
        int Insert(Goal obj);
        Goal Select(int id);
        List<Goal> SelectList();
    }
}
