using CygSoft.SmartSession.Domain.Tasks;
using CygSoft.SmartSession.Repositories.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Repositories.UnitTests.Repository.SQLite
{
    [TestClass]
    public class TaskRepositoryTest
    {
        [TestMethod]
        public void Insert_A_MetronomeTask_Sucessfully()
        {
            var repository = new TaskRepository();
            var taskId = repository.Insert(
                new MetronomeGoalTask
                {
                    Title = "Test Metronome Task"
                }
            );
            Assert.IsTrue(taskId > 0);

            var fetchedTask = repository.Select(taskId);
            Assert.IsNotNull(fetchedTask);
            Assert.IsInstanceOfType(fetchedTask, typeof(MetronomeGoalTask));
        }

        [TestMethod]
        public void Insert_A_DurationTask_Sucessfully()
        {
            var repository = new TaskRepository();
            var taskId = repository.Insert(
                new DurationGoalTask
                {
                    Title = "Test Duration Task"
                }
            );
            Assert.IsTrue(taskId > 0);

            var fetchedTask = repository.Select(taskId);
            Assert.IsNotNull(fetchedTask);
            Assert.IsInstanceOfType(fetchedTask, typeof(DurationGoalTask));
        }

        [TestMethod]
        public void Insert_A_PercentTask_Sucessfully()
        {
            var repository = new TaskRepository();
            var taskId = repository.Insert(
                new PercentGoalTask
                {
                    Title = "Test Percent Task"
                }
            );
            Assert.IsTrue(taskId > 0);

            var fetchedTask = repository.Select(taskId);
            Assert.IsNotNull(fetchedTask);
            Assert.IsInstanceOfType(fetchedTask, typeof(PercentGoalTask));
        }

        [TestMethod]
        public void Insert_A_AggregateTask_Sucessfully()
        {
            var repository = new TaskRepository();
            var taskId = repository.Insert(
                new AggregateTask
                {
                    Title = "Test Aggregate Task"
                }
            );
            Assert.IsTrue(taskId > 0);

            var fetchedTask = repository.Select(taskId);
            Assert.IsNotNull(fetchedTask);
            Assert.IsInstanceOfType(fetchedTask, typeof(AggregateTask));
        }

        //public List<BaseTask> SelectList()
        //{
        //    // arrange
        //    // act 
        //    var taskModel = new TaskRepository().SelectList();

        //    // assert 
        //    Assert.IsTrue(taskModel.Count > 0);

        //    return taskModel;
        //}
        //#endregion
    }
}
