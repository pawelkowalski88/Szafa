using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PresentationUtility.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool input;
            try
            {
                input = (bool)value;
                if (input)
                {
                    return Visibility.Visible;
                }
            }
            catch (Exception){ }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
