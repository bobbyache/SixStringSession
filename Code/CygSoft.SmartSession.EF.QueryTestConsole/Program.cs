using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.EF.QueryTestConsole
{
    /*

    Sometimes works, sometimes doesn't:

    SELECT deqs.last_execution_time AS [Time], dest.text AS [Query]
    FROM sys.dm_exec_query_stats AS deqs
    CROSS APPLY sys.dm_exec_sql_text(deqs.sql_handle) AS dest
    CROSS APPLY sys.dm_exec_plan_attributes(deqs.plan_handle) depa
    WHERE (depa.attribute = 'dbid' AND depa.value = db_id('SmartSession_EF_Test')) OR
                 dest.dbid = db_id('SmartSession_EF_Test')

    Will have to compile test queries here... doesn't work too well with unit tests.

    */

    // Testing an Entity Framework database connection
    // https://stackoverflow.com/questions/19211082/testing-an-entity-framework-database-connection
    // .net core 2.0 ConfigureLogging xunit test
    // https://stackoverflow.com/questions/46169169/net-core-2-0-configurelogging-xunit-test
    // Raw SQL
    // https://docs.microsoft.com/en-us/ef/core/querying/raw-sql
    // Logging with output in Unit Tests in .Net Core 2.0
    //https://alastaircrabtree.com/using-logging-in-unit-tests-in-net-core/


    class Program
    {
        static void Main(string[] args)
        {
            SmartSessionContextFactory factory = new SmartSessionContextFactory();
            var context = factory.CreateDbContext(null);

            var query = context.Exercises
            .Include(ex => ex.ExerciseActivity)
            .Where(ex => ex.Id == 15)
            .Select(ex => new
            {
                Id = ex.Id,
                Title = ex.Title,
                Progress = ex.ExerciseActivity.Sum(act => act.Seconds)
            })
            .ToList();

            Console.ReadLine();
        }
    }
}
