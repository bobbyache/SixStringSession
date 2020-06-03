using System;

namespace SmartClient.Domain.Common
{
    public class Utils
    {
        // Note that here are some other options for enum parsing: https://gist.github.com/pbdesk/1723650
        public static T ParseToEnum<T>(string enumText) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new Exception("T must be an enumerated type.");
            return (T)Enum.Parse(typeof(T), enumText);
        }
    }
}
