using CygSoft.SmartSession.Repositories.Schema;using System.Collections.Generic;

namespace CygSoft.SmartSession.Repositories.Interface
{
  public interface ITaskRepository
  {
    int Insert(TaskRecord obj);
    TaskRecord Select(int id);
    List<TaskRecord> SelectList();
  }
}
