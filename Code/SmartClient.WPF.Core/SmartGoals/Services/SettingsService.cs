using System.Configuration;

namespace SmartGoals.Services
{
    public class SettingsService : ISettingsService
    {
        private const string INITIAL_PROJECT_DIRECTORY = "InitialProjectDirectory";
        public virtual string InitialProjectDirectory
        {
            get
            {
                if (ConfigurationManager.AppSettings[INITIAL_PROJECT_DIRECTORY] != null)
                    return ConfigurationManager.AppSettings[INITIAL_PROJECT_DIRECTORY];
                return "";
            }
            set
            {
                ConfigurationManager.AppSettings[INITIAL_PROJECT_DIRECTORY] = value;
            }
        }
    }
}
