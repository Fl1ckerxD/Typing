using System.Globalization;
using System.Windows.Data;

namespace Typing.Converter
{
    class DateTimeOffsetToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is DateTimeOffset date)
            {
                return date.ToString("d MMMM yyyy HH:mm:ss 'UTC'K", new CultureInfo("ru-RU"));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
