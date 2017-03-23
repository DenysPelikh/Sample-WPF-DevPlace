using System;
using System.Globalization;
using System.Windows.Data;

namespace InteractiveApp.MouseEvents.Common
{
    public class BorderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var relust = (double)value - 20;
            return relust;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack should never be called");
        }
    }
}
