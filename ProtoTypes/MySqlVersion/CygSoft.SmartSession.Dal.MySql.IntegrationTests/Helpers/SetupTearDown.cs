using CygSoft.SmartSession.Dal.MySql.IntegrationTests.Helpers;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Dal.MySql.IntegrationTests
{
    [SetUpFixture]
    public class SetupTearDown
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            Funcs.RunScript("create-database.sql", Settings.SysConnectionString);
            Funcs.RunScript("stored-procs.sql", Settings.AppConnectionString);
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // ...
            // Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);
        }
    }
}
