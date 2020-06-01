using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SmartSession.RecentProjects
{
    public class RecentProjectsRepository
    {
        public virtual void Save(List<RecentProject> recentProjects, string filePath)
        {
            var jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.WriteIndented = false;

            File.WriteAllText(filePath, JsonSerializer.Serialize((List<RecentProject>)recentProjects, jsonSerializerOptions));
        }

        public virtual List<RecentProject> Open(string filePath)
        {
            if (FileExists(filePath))
            {
                return Read(filePath);
            }
            else
            {
                return Create(filePath);
            }
        }

        protected virtual List<RecentProject> Create(string filePath)
        {
            var recentProjects = new List<RecentProject>();
            this.Save(recentProjects, filePath);

            return Read(filePath);
        }

        protected virtual List<RecentProject> Read(string filePath)
        {
            var jsonString = File.ReadAllText(filePath);
            var recentProjects = JsonSerializer.Deserialize<List<RecentProject>>(jsonString);
            return recentProjects;
        }

        protected virtual bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
