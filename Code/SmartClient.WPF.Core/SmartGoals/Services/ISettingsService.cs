using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGoals.Services
{
    public interface ISettingsService
    {
        string InitialProjectDirectory { get; set; }
    }
}
