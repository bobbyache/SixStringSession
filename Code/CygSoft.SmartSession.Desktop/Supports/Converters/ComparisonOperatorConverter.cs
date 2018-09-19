using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CygSoft.SmartSession.Desktop.Supports.Converters
{
    public class ComparisonOperatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var op = (ComparisonOperators)value;

            switch (op)
            {
                case ComparisonOperators.GreaterThan:
                    return ">";
                    
                case ComparisonOperators.GreaterThanOrEqualTo:
                    return ">=";

                case ComparisonOperators.LessThan:
                    return "<";

                case ComparisonOperators.LessThanOrEqualTo:
                    return "<=";

                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string val = (string)value;

            switch (val)
            {
                case ">":
                    return ComparisonOperators.GreaterThan;

                case ">=":
                    return ComparisonOperators.GreaterThan;

                case "<":
                    return ComparisonOperators.GreaterThan;

                case "<=":
                    return ComparisonOperators.GreaterThan;

                default:
                    return ComparisonOperators.Undefined;
            }
        }
    }
}
