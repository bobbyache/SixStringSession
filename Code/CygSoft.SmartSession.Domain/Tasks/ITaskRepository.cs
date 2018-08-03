using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Tasks
{
    public interface ITaskRepository
    {
        int Insert(GoalTaskRecord obj);
        GoalTaskRecord Select(int id);
        List<GoalTaskRecord> SelectList();
    }
}
