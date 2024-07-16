using KinoApiWrapper.Api.RequestSender;
using KinoApiWrapper.ResponseTypes;
using KinoApiWrapper.Utils;
using KinoApiWrapper.Utils.Abstract;
using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiWrapper.Api
{
    public class Movies
    {
        private readonly IRequester requester;
        private readonly IConverter converter;

        internal Movies(IRequester requester, IConverter converter)
        {
            this.requester = requester;
            this.converter = converter;
        }

        public async Task<MovieInfo> GetMovieByIdAsync(int id)
        {
            var result = await requester.Request($@"/api/v2.2/films/{id}");
            if (string.IsNullOrEmpty(result))
                return null;
            return converter.ConvertMovie(result);
        }

        public async Task<Movie[]> GetMovieByYearAsync(int year,int page)
        {
            var result = await requester.Request(@"/api/v2.2/films", new Dictionary<string, string>()
            {
                { "yearFrom",$"{year}" },
                { "yearTo",$"{year}" },
                { "page",$"{page}" },
            }); 
            if (string.IsNullOrEmpty(result))
                return null;
            return converter.ConvertSearchResult(result);
        }

        public async Task<Movie[]> GetMovieByGenreAsync(Genre genre, int page)
        {
            var result = await requester.Request(@"/api/v2.2/films", new Dictionary<string, string>()
            {
                { "genres",$"{genre.Id}" },
                { "page",$"{page}" },
            });
            if (string.IsNullOrEmpty(result))
                return Array.Empty<Movie>();
            return converter.ConvertSearchResult(result);
        }

        public async Task<Movie[]> GetMoviesByKeywordAsync(string keyword, int page)
        {
            var result = await requester.Request(@"/api/v2.1/films/search-by-keyword", new Dictionary<string, string>()
            {
                { "keyword",$"{keyword}" },
                { "page",$"{page}" },
            });
            if (string.IsNullOrEmpty(result))
                return Array.Empty<Movie>();
            return converter.ConvertSearchByKeywordResult(result);
        }
    }
}
