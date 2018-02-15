using System;
using System.Globalization;
using System.Windows.Data;

namespace PresentationUtility.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                DateTime d = (DateTime)value;
                return "od " + d.Day.ToString() + "-" + d.Month.ToString() + "-" + d.Year.ToString();
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
