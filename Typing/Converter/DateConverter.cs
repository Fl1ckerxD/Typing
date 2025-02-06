using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Typing.Converter
{
    internal class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTimeOffset date)
            {
                var diff = DateTimeOffset.Now - date;
                if (diff < TimeSpan.FromMinutes(2))
                    return "сейчас";
                else if (diff < TimeSpan.FromHours(1))
                    return $"{diff.Minutes.ToString()}мин";
                else if (diff < TimeSpan.FromDays(1))
                    return $"{diff.Hours.ToString()}ч";
                else if (diff < TimeSpan.FromDays(31))
                    return $"{diff.Days.ToString()}д";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
