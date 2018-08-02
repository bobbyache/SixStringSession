using CygSoft.SmartSession.BaseRepository.Interface;
using CygSoft.SmartSession.BaseRepository.Schema;
using CygSoft.SmartSession.BaseRepository.SQLite;
using CygSoft.SmartSession.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CygSoft.SmartSession.DAL.Tests
{
    [TestClass]
    public class UserRepositoryTest : IUserRepository
    {
        [TestMethod]
        public void UserRepository_Insert()
        {
            Insert(null);
        }

        [TestMethod]
        public void UserRepository_Select()
        {
            Select(820000000, "password1");
        }

        #region IUserRepository
        public UserModel Select(long cellPhone, string password)
        {
            // arrange
            var _cellPhone = cellPhone;
            var _password = new PasswordHash().Go(password); //"7C6A180B36896A0A8C02787EEAFB0E4C"

            // act 
            var dbModel = new UserRepository().Select(cellPhone, _password);

            // assert 
            Assert.IsTrue(dbModel.Id > 0);

            return dbModel;
        }

        public int Insert(UserModel obj)
        {
            // arrange
            var password = new PasswordHash().Go("password1"); //"7C6A180B36896A0A8C02787EEAFB0E4C"
            var dbModel = new UserModel()
            {
                CellPhone = 123456789,
                HasAccess = true,
                Name = "Name",
                Password = password,
                Surname = "Surname"
            };

            // act 
            var newId = new UserRepository().Insert(dbModel);

            // assert 
            Assert.IsTrue(newId > 0);

            return newId;
        }
        #endregion
    }
}
