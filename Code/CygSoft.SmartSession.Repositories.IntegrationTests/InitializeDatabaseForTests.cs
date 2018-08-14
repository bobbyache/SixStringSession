using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CygSoft.SmartSession.Repositories.IntegrationTests
{
    [SetUpFixture]
    public class InitializeDatabaseForTests
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            RunScript("CreateDatabase.sql", @"server=ROBB-LT02\ROBLT;database=master;Integrated Security=True;Connection Reset=true;");
            RunScript("CreateStructure.sql", @"server=ROBB-LT02\ROBLT;database=SmartSession_UnitTests;Integrated Security=True;Connection Reset=true;");
            RunScript("PopulateLookupTables.sql", @"server=ROBB-LT02\ROBLT;database=SmartSession_UnitTests;Integrated Security=True;Connection Reset=true;");
            RunScripts(GetStoredProcedureScriptFiles(), @"server=ROBB-LT02\ROBLT;database=SmartSession_UnitTests;Integrated Security=True;Connection Reset=true;");
            RunScript("PopulateTestData.sql", @"server=ROBB-LT02\ROBLT;database=SmartSession_UnitTests;Integrated Security=True;Connection Reset=true;");
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // ...
        }

        private string[] GetStoredProcedureScriptFiles()
        {
            string[] files = Directory.GetFiles(Path.Combine(ScriptFile.GetFolder(), "SPs"), "*.sql")
                .Select(f => Path.Combine("SPs", Path.GetFileName(f))).ToArray();

            return files;
        }

        private static void RunScript(string scriptFile, string connectionString)
        {
            string script = ScriptFile.ReadText(scriptFile);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(script, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private static void RunScripts(string[] scriptFiles, string connectionString)
        {
            List<string> scripts = new List<string>();

            foreach (string scriptFile in scriptFiles)
            {
                scripts.Add(ScriptFile.ReadText(scriptFile));
            }

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (string script in scripts)
                {
                    using (var command = new SqlCommand(script, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }


}
