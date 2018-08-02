using CygSoft.SmartSession.Repositories.Schema;using System.Collections.Generic;

namespace CygSoft.SmartSession.Repositories.Interface
{
    public interface IGoalRepository
    {
        int Insert(GoalRecord obj);
        GoalRecord Select(int id);
        List<GoalRecord> SelectList();
    }
}
