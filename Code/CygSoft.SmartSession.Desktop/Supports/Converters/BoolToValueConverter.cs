using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CygSoft.SmartSession.Desktop.Supports.Converters
{
    public class BoolToValueConverter<T> : IValueConverter
    {
        public T FalseValue { get; set; }
        public T TrueValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return FalseValue;
            else
                return (bool)value ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null ? value.Equals(TrueValue) : false;
        }
    }

    public class BoolToStringConverter : BoolToValueConverter<String> { }

    // http://geekswithblogs.net/codingbloke/archive/2010/05/28/a-generic-boolean-value-converter.aspx

    //public class BoolToBrushConverter : BoolToValueConverter<Brush> { }
    //public class BoolToVisibilityConverter : BoolToValueConverter<Visibility> { }
    //public class BoolToObjectConverter : BoolToValueConverter<Object> { }

/*
 * <local:BoolToBrushConverter x:Key="Highlighter" FalseValue="Transparent" TrueValue="Yellow" />
<local:BoolToStringConverter x:Key="CYesNo" FalseValue="No" TrueValue="Yes" />
<local:BoolToVisibilityConverter x:Key="InverseVisibility" TrueValue="Collapsed" FalseValue="Visible" />
*/
}
