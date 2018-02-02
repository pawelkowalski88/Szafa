using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PresentationUtility.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility v = new Visibility();
            //DataRowView dr = parameter as DataRowView;
            if (value == null)
            {
                v = Visibility.Hidden;
            }
            else
            {
                v = Visibility.Visible;
            }
            return v;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
