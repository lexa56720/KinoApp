using KinoTypes;
using System;
using System.Linq;
using Windows.UI.Xaml.Data;

namespace KinoApp.Helpers
{
    internal class GenresToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Genre[] genres)
            {
                return string.Join(", ", genres.Select(c => c.Name).ToArray());
            }

            return "-";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
