using CygSoft.SmartSession.BaseRepository.Schema;
using System.Collections.Generic;

namespace CygSoft.SmartSession.BaseRepository.Interface
{
    public interface IGoalRepository
    {
        int Insert(GoalModel obj);
        GoalModel Select(int id);
        List<GoalModel> SelectList();
    }
}
