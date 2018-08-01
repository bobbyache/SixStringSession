using CygSoft.SmartSession.DAL.Repository.Schema;
using System.Collections.Generic;

namespace CygSoft.SmartSession.DAL.Repository.Interface
{
    public interface IGoalRepository
    {
        int Insert(GoalModel obj);
        GoalModel Select(int id);
        List<GoalModel> SelectList();
    }
}
