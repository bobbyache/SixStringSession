using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MvvmLight_Prototypes.Converters
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
