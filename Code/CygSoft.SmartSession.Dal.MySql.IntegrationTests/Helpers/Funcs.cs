using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Dal.MySql.IntegrationTests.Helpers
{
    public class Funcs
    {
        public static DateTime RemoveMilliSeconds(DateTime dateTime)
        {
            return DateTime.Parse(DateTime.Now.ToString("yyy/MM/dd hh:mm:ss"));
        }
    }
}
