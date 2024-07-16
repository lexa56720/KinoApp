using KinoApiWrapper.Api;
using KinoApiWrapper.ResponseTypes;
using KinoApiWrapper.ResponseTypes.MoviesTypes;
using KinoApiWrapper.ResponseTypes.MoviesTypes.Keyword;
using KinoApiWrapper.ResponseTypes.MoviesTypes.Search;
using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiWrapper.Utils.Abstract
{
    internal interface IMapper
    {
        Movie Map(MovieSearch response);

        MovieInfo Map(FullMovieInfo response);

        Movie Map(MovieKeyword response);

        Genre Map(ApiGenre response);
    }
}
