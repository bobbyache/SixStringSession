using CygSoft.SmartSession.Dal.MySql.IntegrationTests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Dal.MySql.IntegrationTests.Tests
{
    [TestFixture]
    public class PracticeRoutineRepositoryTests
    {
        [Test]
        public void RunTest()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);
            Funcs.RunScript("test-data.sql", Settings.AppConnectionString);
        }
    }
}
