using KinoApiWrapper.Api.RequestSender;
using KinoApiWrapper.Utils.Abstract;
using KinoTypes;
using KinoTypes.DataProvider;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KinoApiWrapper.Api
{
    public class Movies : IMovies
    {
        private enum MovieCollections
        {
            TOP_POPULAR_ALL,
            TOP_POPULAR_MOVIES,
            TOP_250_TV_SHOWS,
            TOP_250_MOVIES,
            VAMPIRE_THEME,
            COMICS_THEME,
            CLOSES_RELEASES,
            FAMILY,
            OSKAR_WINNERS_2021,
            LOVE_THEME,
            ZOMBIE_THEME,
            CATASTROPHE_THEME,
            KIDS_ANIMATION_THEME,
            POPULAR_SERIES
        }

        private readonly IRequester requester;
        private readonly IConverter converter;

        internal Movies(IRequester requester, IConverter converter)
        {
            this.requester = requester;
            this.converter = converter;
        }

        //Получение фильма по ID
        public async Task<MovieInfo> GetMovieByIdAsync(int id)
        {
            var result = await requester.Request($@"/api/v2.2/films/{id}");
            if (string.IsNullOrEmpty(result))
                return null;
            return converter.ConvertMovie(result);
        }

        //Получение фильмов по ID
        public async Task<MovieInfo[]> GetMovieByIdAsync(int[] ids)
        {
            var funcs = new Func<int, Task<MovieInfo>>[ids.Length];
            for (int i = 0; i < ids.Length; i++)
            {
                funcs[i] = (int j) => GetMovieByIdAsync(ids[j]);
            }
            //Ограничение на количество запросов в секунду
            var movies = await RequestLimiter.Call(funcs, 15);
            return movies;
        }

        //Получение топа лучших фильмов
        public async Task<Movie[]> GetBestMoviesAsync(int page)
        {
            return await GetMovieCollectionAsync(MovieCollections.TOP_250_MOVIES, page);
        }

        //Получение колекции фильмов
        private async Task<Movie[]> GetMovieCollectionAsync(MovieCollections collection, int page)
        {
            var result = await requester.Request($@"/api/v2.2/films/collections?type={collection}&page={page}");
            if (string.IsNullOrEmpty(result))
                return null;
            return converter.ConvertSearchResult(result);
        }

        //Получение фильмов в соответсвии с параметрами
        public async Task<Movie[]> GetMoviesFilteredAsync(int? yearFrom, int? yearTo, Genre genre, Order? order, string keyword, int page)
        {
            //Аргументы запроса
            var args = new Dictionary<string, string>
            {
                { "page", $"{page}" },
                { "yearFrom", yearFrom == null ? "1000" : yearFrom.ToString() },
                { "yearTo", yearTo == null ? "3000" : yearTo.ToString() },
                { "order", order == null ? Order.RATING.ToString() : order.ToString() }
            };

            if (genre != null)
                args.Add("genres", genre.Id.ToString());

            if (!string.IsNullOrEmpty(keyword))
                args.Add("keyword", keyword);

            var result = await requester.Request(@"/api/v2.2/films", args);
            if (string.IsNullOrEmpty(result))
                return null;
            return converter.ConvertSearchResult(result);
        }
    }
}
