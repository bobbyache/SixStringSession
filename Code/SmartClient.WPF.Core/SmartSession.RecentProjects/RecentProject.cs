using System.IO;
using System.Text.Json.Serialization;

namespace SmartSession.RecentProjects
{
    public class RecentProject
    {
        public RecentProject()
        {
        }
        public RecentProject(string filePath)
        {
            this.FilePath = filePath;
        }

        [JsonIgnore()]
        public string FileTitle { get { return Path.GetFileName(FilePath); } }

        [JsonPropertyName("file")]
        public string FilePath { get; set; }
    }
}
