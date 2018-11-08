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
            RunScript("create-database.sql", Settings.SysConnectionString);
            RunScript("stored-procs.sql", Settings.AppConnectionString);
            RunScript("test-data.sql", Settings.AppConnectionString);
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // ...
        }

        // If you want to run all files without having to add them manually! 

        //private string[] GetStoredProcedureScriptFiles()
        //{
        //    string[] files = Directory.GetFiles(Path.Combine(ScriptFile.GetFolder(), "SPs"), "*.sql")
        //        .Select(f => Path.Combine("SPs", Path.GetFileName(f))).ToArray();

        //    return files;
        //}

        private static void RunScript(string scriptFile, string connectionString)
        {
            string script = ScriptFile.ReadText(scriptFile);

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new MySqlCommand(script, connection))
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

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                foreach (string script in scripts)
                {
                    using (var command = new MySqlCommand(script, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
