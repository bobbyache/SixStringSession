using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Tasks
{
    public interface ITaskRepository
    {
        int Insert(BaseTask obj);

        BaseTask Select(int id);
        List<BaseTask> SelectList();
    }
}
