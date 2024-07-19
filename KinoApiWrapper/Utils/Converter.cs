using KinoApiWrapper.ResponseTypes;
using KinoApiWrapper.ResponseTypes.MoviesTypes;
using KinoApiWrapper.ResponseTypes.MoviesTypes.Search;
using KinoApiWrapper.Utils;
using KinoApiWrapper.Utils.Abstract;
using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KinoApiWrapper.Api
{
    internal class Converter: IConverter
    {
        private readonly IMapper mapper;

        public Converter(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public Genre[] ConvertGenres(string json)
        {
            var filters = JsonSerializer.Deserialize<FilterResponse>(json);
            return filters.Genres.Select(mapper.Map).ToArray();
        }

        public MovieInfo ConvertMovie(string json)
        {
            var movieInfo = JsonSerializer.Deserialize<FullMovieInfo>(json);
            return mapper.Map(movieInfo);
        }
        public Movie[] ConvertSearchResult(string json)
        {
            var searchResult = JsonSerializer.Deserialize<MovieSearchResponse>(json);
            return searchResult.Items.Select(mapper.Map).ToArray();
        }
    }
}
