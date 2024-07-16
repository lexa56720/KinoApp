using KinoApiWrapper.ResponseTypes;
using KinoApiWrapper.Utils;
using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KinoApiWrapper.Api
{
    internal class Converter
    {
        private readonly Mapper mapper;

        public Converter(Mapper mapper)
        {
            this.mapper = mapper;
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
