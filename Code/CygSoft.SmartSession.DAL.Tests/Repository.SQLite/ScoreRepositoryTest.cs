using CygSoft.SmartSession.Repositories.Interface;
using CygSoft.SmartSession.Repositories.Schema;
using CygSoft.SmartSession.Repositories.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Repositories.UnitTests.Repository.SQLite
{
    [TestClass]
    public class ScoreRepositoryTest : IScoreRepository
    {
        [TestMethod]
        public void ScoreRepository_Insert()
        {
            Insert(null);
        }

        [TestMethod]
        public void ScoreRepository_Select()
        {
            Select(1);
        }

        [TestMethod]
        public void ScoreRepository_SelectList()
        {
            SelectList();
        }

        #region IScoreRepository
        public int Insert(ScoreRecord obj)
        {
            // arrange
            var dbModel = new ScoreRecord()
            {
                InsertDate = DateTime.Now,
                RestaurantId = 1,
                Taste = 4,
                Temperature = 5,
                Tomorrow = 6,
                UserId = 1
            };

            // act 
            var newId = new ScoreRepository().Insert(dbModel);

            // assert 
            Assert.IsTrue(newId > 0);

            return newId;
        }

        public ScoreRecord Select(int id)
        {
            // arrange
            int _id = id;

            // act 
            var dbModel = new ScoreRepository().Select(_id);

            // assert 
            Assert.IsTrue(dbModel.Id > 0);

            return dbModel;
        }

        public List<ScoreRecord> SelectList()
        {
            // arrange
            // act 
            var dbModel = new ScoreRepository().SelectList();

            // assert 
            Assert.IsTrue(dbModel.Count > 0);

            return dbModel;
        }
        #endregion
    }
}
