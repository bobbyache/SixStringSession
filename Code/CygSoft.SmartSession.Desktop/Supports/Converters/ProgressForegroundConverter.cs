using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CygSoft.SmartSession.Desktop.Supports.Converters
{
    public class ProgressForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int progress = (int)value;
            Brush foreground = Brushes.Green;

            if (progress >= 90d)
            {
                foreground = Brushes.Red;
            }
            else if (progress >= 60d)
            {
                foreground = Brushes.Yellow;
            }

            return foreground;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
