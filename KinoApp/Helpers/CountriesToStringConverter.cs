using KinoTypes;
using System;
using System.Linq;
using Windows.UI.Xaml.Data;

namespace KinoApp.Helpers
{
    public class CountriesToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Country[] countries)
            {
                return string.Join(", ", countries.Select(c => c.Name).ToArray());
            }
            return "-";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
