using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Dal.MySql.IntegrationTests.Helpers
{
    public class ScriptFile
    {

        public static string GetFolder()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Scripts");
        }

        public static string ResolvePath(string fileName)
        {
            return Path.Combine(GetFolder(), fileName);
        }

        public static string ReadText(string fileName)
        {

            return File.ReadAllText(ResolvePath(fileName));
        }
    }
}
