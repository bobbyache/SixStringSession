using CygSoft.SmartSession.Repositories.Schema;using System.Collections.Generic;

namespace CygSoft.SmartSession.Repositories.Interface
{
    public interface IGoalRepository
    {
        int Insert(GoalModel obj);
        GoalModel Select(int id);
        List<GoalModel> SelectList();
    }
}
