using CygSoft.SmartSession.Repositories.Schema;using System.Collections.Generic;

namespace CygSoft.SmartSession.Repositories.Interface{
  public interface ITaskRepository
  {
    int Insert(TaskModel obj);
    TaskModel Select(int id);
    List<TaskModel> SelectList();
  }
}
