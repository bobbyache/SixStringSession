using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Infrastructure
{
    public static class TimeFuncs
    {
        public static string ZeroTimeDisplay = "00:00:00";
        public static string DisplayTimeFromSeconds(double seconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            return t.ToString(@"hh\:mm\:ss");
        }

        public static string DisplayTimeFromSeconds(double? seconds)
        {
            if (seconds.HasValue)
                return DisplayTimeFromSeconds(seconds.Value);
            else
                return ZeroTimeDisplay;
        }
    }
}
