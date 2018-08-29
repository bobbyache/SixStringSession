using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CygSoft.SmartSession.Desktop.Supports.Converters
{
    public class OnScheduleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isOnSchedule = (bool)value;
            Brush brush = Brushes.Green;

            if (!isOnSchedule)
                brush = Brushes.Red;

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
