using System.Windows;
using System.Windows.Data;

namespace CygSoft.SmartSession.Desktop.Supports.Converters
{
    public class StarWidthConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                var stars = System.Convert.ToDecimal(value);
                var width = (double)(80 / 5 * stars);
                return new Rect(0, 0, width, 16);
            }
            catch
            {
                return new Rect(0, 0, 80, 16);
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
