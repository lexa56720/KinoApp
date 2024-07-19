using KinoApiWrapper.Api.RequestSender;
using KinoApiWrapper.ResponseTypes;
using KinoApiWrapper.Utils;
using KinoApiWrapper.Utils.Abstract;
using KinoTypes;
using KinoTypes.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiWrapper.Api
{
    public class Movies:IMovies
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
        public async Task<MovieInfo[]> GetMovieByIdAsync(int[] ids)
        {
            var funcs = new Func<int,Task<MovieInfo>>[ids.Length];
            for (int i = 0; i < ids.Length; i++)
            {
                funcs[i] = (int j) => GetMovieByIdAsync(ids[j]);
            }
            var movies = await RequestLimiter.Call(funcs, 10);
            return movies;
        }

        public async Task<Movie[]> GetMovieByYearAsync(int year, Order order = Order.RATING, int page = 1)
        {
            var result = await requester.Request(@"/api/v2.2/films", new Dictionary<string, string>()
            {
                { "yearFrom",$"{year}" },
                { "yearTo",$"{year}" },
                { "order",$"{order.ToString()}" },
                { "page",$"{page}" },
            });
            if (string.IsNullOrEmpty(result))
                return null;
            return converter.ConvertSearchResult(result);
        }

        public async Task<Movie[]> GetMovieByGenreAsync(Genre genre, Order order = Order.RATING, int page = 1)
        {
            var result = await requester.Request(@"/api/v2.2/films", new Dictionary<string, string>()
            {
                { "genres",$"{genre.Id}" },
                { "order",$"{order.ToString()}" },
                { "page",$"{page}" },
            });
            if (string.IsNullOrEmpty(result))
                return null;
            return converter.ConvertSearchResult(result);
        }

        public async Task<Movie[]> GetMoviesByKeywordAsync(string keyword, int page = 1)
        {
            var result = await requester.Request(@"/api/v2.1/films/search-by-keyword", new Dictionary<string, string>()
            {
                { "keyword",$"{keyword}" },
                { "page",$"{page}" },
            });
            if (string.IsNullOrEmpty(result))
                return null;
            return converter.ConvertSearchByKeywordResult(result);
        }
    }
}
