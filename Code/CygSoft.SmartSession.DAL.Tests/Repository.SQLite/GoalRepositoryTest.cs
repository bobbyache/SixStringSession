using CygSoft.SmartSession.Repositories.Interface;
using CygSoft.SmartSession.Repositories.Schema;
using CygSoft.SmartSession.Repositories.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Repositories.UnitTests.Repository.SQLite
{
    [TestClass]
    public class GoalRepositoryTest : IGoalRepository
    {
        [TestMethod]
        public void GoalRepository_Insert()
        {
            Insert(null);
        }

        [TestMethod]
        public void GoalRepository_Select()
        {
            Select(1);
        }

        [TestMethod]
        public void GoalRepository_SelectList()
        {
            SelectList();
        }

        public int Insert(GoalModel obj)
        {
            // arrange
            var goalModel = new GoalModel()
            {
                Name = "Goal Name"
            };

            // act 
            var newId = new GoalRepository().Insert(goalModel);

            // assert 
            Assert.IsTrue(newId > 0);

            return newId;
        }

        public GoalModel Select(int id)
        {
            // arrange
            int _id = id;

            // act 
            var goalModel = new GoalRepository().Select(_id);

            // assert 
            Assert.IsTrue(goalModel.Id > 0);

            return goalModel;
        }

        public List<GoalModel> SelectList()
        {
            // arrange
            // act 
            var goalModel = new GoalRepository().SelectList();

            // assert 
            Assert.IsTrue(goalModel.Count > 0);

            return goalModel;
        }
    }
}
