using KinoApiWrapper.ResponseTypes;
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
        public Converter()
        {
        }

        public Movie ConvertMovie(string json)
        {
            var movieInfo = JsonSerializer.Deserialize<FullMovieInfo>(json);
            return Map(movieInfo);
        }
        public Movie[] ConvertSearchResult(string json)
        {
            var searchResult = JsonSerializer.Deserialize<MovieSearchResponse>(json);
            return searchResult.Items.Select(Map).ToArray();
        }

        private Movie Map(BriefMovieInfo response)
        {
            throw new NotImplementedException();
        }
        private Movie Map(FullMovieInfo response)
        {
            throw new NotImplementedException();
        }
    }
}
