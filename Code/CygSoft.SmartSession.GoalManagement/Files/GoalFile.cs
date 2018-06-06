using CygSoft.SmartSession.GoalManagement.Infrastructure;
using System.IO;

namespace CygSoft.SmartSession.GoalManagement.Files
{
    public class GoalFile : IGoalFile
    {
        public bool Exists => File.Exists(FilePath);

        public string Extension => Path.GetExtension(FilePath);

        public string FileName => Path.GetFileName(FilePath);

        public string FilePath { get; set; }
    }
}
