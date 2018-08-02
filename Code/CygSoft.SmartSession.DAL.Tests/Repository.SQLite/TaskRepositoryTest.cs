using CygSoft.SmartSession.Repositories.Interface;
using CygSoft.SmartSession.Repositories.Schema;
using CygSoft.SmartSession.Repositories.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Repositories.UnitTests.Repository.SQLite
{
  [TestClass]
  public class TaskRepositoryTest : ITaskRepository
  {
    [TestMethod]
    public void TaskRepository_Insert()
    {
      Insert(null);
    }

    [TestMethod]
    public void TaskRepository_Select()
    {
      Select(1);
    }

    [TestMethod]
    public void TaskRepository_SelectList()
    {
      SelectList();
    }

    #region ITaskRepository
    public int Insert(TaskRecord obj)
    {
      // arrange
      var taskModel = new TaskRecord()
      {
        Name = "Task Name"
      };

      // act 
      var newId = new TaskRepository().Insert(taskModel);

      // assert 
      Assert.IsTrue(newId > 0);

      return newId;
    }

    public TaskRecord Select(int id)
    {
      // arrange
      int _id = id;

      // act 
      var taskModel = new TaskRepository().Select(_id);

      // assert 
      Assert.IsTrue(taskModel.Id > 0);

      return taskModel;
    }

    public List<TaskRecord> SelectList()
    {
      // arrange
      // act 
      var taskModel = new TaskRepository().SelectList();

      // assert 
      Assert.IsTrue(taskModel.Count > 0);

      return taskModel;
    }
    #endregion
  }
}
