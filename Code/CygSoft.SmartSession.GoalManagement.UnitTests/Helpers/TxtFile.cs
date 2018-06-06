using System.IO;
using System.Reflection;

namespace CygSoft.SmartSession.GoalManagement.UnitTests.Helpers
{
    public class TxtFile
    {
        
        public static string GetFolder()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Files");
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
