using System;
using System.Collections.Generic;
using System.Text;

namespace SmartClient.Domain.Common
{
    public class Utils
    {
        public static T ParseToEnum<T>(string enumText) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new Exception("T must be an enumerated type.");
            return (T)Enum.Parse(typeof(T), enumText);
        }
    }
}
