using CygSoft.SmartSession.BaseRepository.Schema;
using System.Collections.Generic;

namespace CygSoft.SmartSession.BaseRepository.Interface
{
  public interface ITaskRepository
  {
    int Insert(TaskModel obj);
    TaskModel Select(int id);
    List<TaskModel> SelectList();
  }
}
