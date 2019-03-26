using CygSoft.SmartSession.Dal.MySql.Repositories;
using CygSoft.SmartSession.Domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Dal.MySql.IntegrationTests.Helpers
{
    public class Funcs
    {
        public static IUnitOfWork GetUnitOfWork()
        {
            var connection = UnitOfWork.CreateConnection(Settings.AppConnectionString);
            var uow = new UnitOfWork(connection, new ExerciseRepository(connection), new PracticeRoutineRepository(connection));
            return uow;
        }

        public static DateTime RemoveMilliSeconds(DateTime dateTime)
        {
            return DateTime.Parse(DateTime.Now.ToString("yyy/MM/dd hh:mm:ss"));
        }

        // If you want to run all files without having to add them manually! 

        //private string[] GetStoredProcedureScriptFiles()
        //{
        //    string[] files = Directory.GetFiles(Path.Combine(ScriptFile.GetFolder(), "SPs"), "*.sql")
        //        .Select(f => Path.Combine("SPs", Path.GetFileName(f))).ToArray();

        //    return files;
        //}

        public static void RunScript(string scriptFile, string connectionString)
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

        public static void RunScripts(string[] scriptFiles, string connectionString)
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
