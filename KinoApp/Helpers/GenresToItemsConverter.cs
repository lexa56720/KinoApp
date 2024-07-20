using KinoTypes;
using System;
using System.Linq;
using Windows.UI.Xaml.Data;

namespace KinoApp.Helpers
{
    public class GenresToItemsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Genre[] genres)
            {
                return genres.Select(g => g.Name).ToArray();
            }
            return "-";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
