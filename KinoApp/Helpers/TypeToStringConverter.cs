using KinoTypes;
using System;
using Windows.UI.Xaml.Data;

namespace KinoApp.Helpers
{
    internal class TypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is MovieType type)
            {
                switch (type)
                {
                    case MovieType.FILM:
                        return "фильме";
                    case MovieType.VIDEO:
                        return "видео";
                    case MovieType.TV_SERIES:
                        return "сериале";
                    case MovieType.MINI_SERIES:
                        return "мини-сериале";
                    case MovieType.TV_SHOW:
                        return "ТВ-шоу";
                }
            }
            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
