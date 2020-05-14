using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace SmartGoals.Services
{
    public class SettingsService : ISettingsService
    {
        public virtual string InitialProjectDirectory
        {
            get
            {
                if (ConfigurationManager.AppSettings["InitialProjectDirectory"] != null)
                    return ConfigurationManager.AppSettings["InitialProjectDirectory"];
                return "";
            }
            set
            {
                ConfigurationManager.AppSettings["InitialProjectDirectory"] = value;
            }
        }
    }
}
