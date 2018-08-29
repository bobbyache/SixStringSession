using System.Windows.Data;

namespace CygSoft.SmartSession.Desktop.Supports.Converters
{
    public class StarHeightConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                var stars = (decimal)value;
                var height = (double)(200 / 5 * stars);
                return height;
            }
            catch
            {
                return 200d;
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
