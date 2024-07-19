using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace KinoApp.Helpers
{
    internal class MinutesToTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int minutes)
            {
                var time = TimeSpan.FromMinutes(minutes);
                if (time.Hours != 0)
                    return string.Format("{0:%h} ч {0:%m} мин",time);
                else
                    return string.Format("{0:%m} мин", time);

            }
            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
