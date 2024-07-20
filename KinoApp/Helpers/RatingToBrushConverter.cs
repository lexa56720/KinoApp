using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace KinoApp.Helpers
{
    internal class RatingToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double rating)
            {
                byte red = (byte)(255 * (1 - rating / 10));
                byte green = (byte)(255 * (rating / 10));
                byte blue = 0;

                return new SolidColorBrush(new Color()
                {
                    A = 255,
                    R = red,
                    G = green,
                    B = blue,
                });
            }
            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
