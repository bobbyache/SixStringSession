using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.EF
{
    public class SmartSessionContextFactory : IDesignTimeDbContextFactory<SmartSessionContext>
    {
        // https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation

        public SmartSessionContext CreateDbContext(string[] args)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<SmartSession>();
            //optionsBuilder.UseSqlite("Data Source=blog.db");

            return new SmartSessionContext(@"server=ROBB-LT02\ROBLT;database=SmartSession_EF_Test;Integrated Security=True;Connection Reset=true;");
        }
    }
}
