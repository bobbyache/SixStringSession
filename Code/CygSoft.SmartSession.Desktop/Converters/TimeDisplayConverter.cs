using System;
using System.Globalization;
using System.Windows.Data;

namespace MvvmLight_Prototypes.Converters
{
    public class TimeDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan timespan = TimeSpan.FromSeconds(System.Convert.ToDouble(value));
            if (timespan.Hours > 0)
                return timespan.ToString(@"hh\:mm\:ss");
            else
                return timespan.ToString(@"mm\:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
