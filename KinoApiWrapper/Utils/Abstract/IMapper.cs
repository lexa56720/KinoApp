using KinoApiWrapper.ResponseTypes;
using KinoApiWrapper.ResponseTypes.MoviesTypes;
using KinoApiWrapper.ResponseTypes.MoviesTypes.Search;
using KinoTypes;

namespace KinoApiWrapper.Utils.Abstract
{
    internal interface IMapper
    {
        Movie Map(MovieSearch response);

        MovieInfo Map(FullMovieInfo response);

        Genre Map(ApiGenre response);
    }
}
