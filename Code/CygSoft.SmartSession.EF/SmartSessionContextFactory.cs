using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.EF
{
    /// <summary>
    /// Source: https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation
    /// 
    /// This class is required for design time updates to the database schema. Therefore, it is ok to hard code the connection
    /// string because the "update" will only go to the test database (as configured below). This connection string does not 
    /// affect the connection for the application, which is stored in the GUI App.Config under connection strings.
    /// 
    /// To update the production database, do a "script-migration" and execute the updates related to specific release.
    /// </summary>
    public class SmartSessionContextFactory : IDesignTimeDbContextFactory<SmartSessionContext>
    {
        public SmartSessionContext CreateDbContext(string[] args)
        {
            return new SmartSessionContext(@"server=ROBB-LT02\ROBLT;database=SmartSession_EF_Test;Integrated Security=True;Connection Reset=true;");
        }
    }
}
