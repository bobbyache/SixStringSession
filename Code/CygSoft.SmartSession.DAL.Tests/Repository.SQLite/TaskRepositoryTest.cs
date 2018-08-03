using CygSoft.SmartSession.Domain.Tasks;
using CygSoft.SmartSession.Repositories.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public int Insert(GoalTaskRecord obj)
        {
            // arrange
            var taskModel = new GoalTaskRecord()
            {
                Title = "Task Name"
            };

            // act 
            var newId = new TaskRepository().Insert(taskModel);

            // assert 
            Assert.IsTrue(newId > 0);

            return newId;
        }

        public GoalTaskRecord Select(int id)
        {
            // arrange
            int _id = id;

            // act 
            var taskModel = new TaskRepository().Select(_id);

            // assert 
            Assert.IsTrue(taskModel.Id > 0);

            return taskModel;
        }

        public List<GoalTaskRecord> SelectList()
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
