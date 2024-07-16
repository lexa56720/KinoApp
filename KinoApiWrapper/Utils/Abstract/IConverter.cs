using KinoApiWrapper.Api;
using KinoApiWrapper.ResponseTypes;
using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KinoApiWrapper.Utils.Abstract
{
    internal interface IConverter
    {
        MovieInfo ConvertMovie(string json);

        Movie[] ConvertSearchResult(string json);

        Genre[] ConvertGenres(string json);

        Movie[] ConvertSearchByKeywordResult(string json);
    }
}
