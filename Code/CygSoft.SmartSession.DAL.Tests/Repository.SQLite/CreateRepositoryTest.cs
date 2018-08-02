using Microsoft.VisualStudio.TestTools.UnitTesting;
using CygSoft.SmartSession.DAL.Repository.SQLite;

namespace CygSoft.SmartSession.DAL.Tests
{
    /// <summary>
    /// The schema can also be stored as .sql scripts ~ this was simpler
    /// </summary>
    [TestClass]
    public class CreateRepositoryTest
    {
        [TestMethod]
        public void CreateRepository_Structure()
        {
            new DropAndCreateRepository().Goal();
            new DropAndCreateRepository().Task();
            new DropAndCreateRepository().User();
            new DropAndCreateRepository().Restaurant();
            new DropAndCreateRepository().Score();

            //probably should assert something \:D/
        }

        [TestMethod]
        public void CreateRepository_Seed()
        {
            new SeedRepository().Goal();
            new SeedRepository().Task();
            new SeedRepository().User();
            new SeedRepository().Restaurant();
            new SeedRepository().Score();

            //probably should assert something \:D/
        }
    }
}
