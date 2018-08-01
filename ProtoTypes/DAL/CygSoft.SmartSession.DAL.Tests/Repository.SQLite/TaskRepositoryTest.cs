using CygSoft.SmartSession.DAL.Repository.Interface;
using CygSoft.SmartSession.DAL.Repository.Schema;
using CygSoft.SmartSession.DAL.Repository.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.DAL.Tests.Repository.SQLite
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
    public int Insert(TaskModel obj)
    {
      // arrange
      var taskModel = new TaskModel()
      {
        Name = "Task Name"
      };

      // act 
      var newId = new TaskRepository().Insert(taskModel);

      // assert 
      Assert.IsTrue(newId > 0);

      return newId;
    }

    public TaskModel Select(int id)
    {
      // arrange
      int _id = id;

      // act 
      var taskModel = new TaskRepository().Select(_id);

      // assert 
      Assert.IsTrue(taskModel.Id > 0);

      return taskModel;
    }

    public List<TaskModel> SelectList()
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
