using System;
using System.Globalization;
using System.Windows.Data;

namespace PresentationUtility.Converters
{
    public class DateToDaysConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                DateTime d = (DateTime)value;
                int dayspassed = DateTime.Now.Subtract(d).Days;
                if (dayspassed < 0)
                {
                    dayspassed = 0;
                }
                if (dayspassed == 1)
                {
                    return dayspassed + " dzień";
                }
                else
                {
                    return dayspassed + " dni";
                }
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
