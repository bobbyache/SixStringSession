using CygSoft.SmartSession.DAL.Repository.Schema;
using System.Collections.Generic;

namespace CygSoft.SmartSession.DAL.Repository.Interface
{
  public interface ITaskRepository
  {
    int Insert(TaskModel obj);
    TaskModel Select(int id);
    List<TaskModel> SelectList();
  }
}
