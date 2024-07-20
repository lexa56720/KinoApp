using KinoTypes;

namespace KinoApiWrapper.Utils.Abstract
{
    internal interface IConverter
    {
        MovieInfo ConvertMovie(string json);

        Movie[] ConvertSearchResult(string json);

        Genre[] ConvertGenres(string json);
    }
}
