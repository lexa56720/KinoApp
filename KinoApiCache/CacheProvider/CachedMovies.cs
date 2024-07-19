using KinoApiCache.DataBase;
using KinoApiCache.DataBase.Interaction;
using KinoApiCache.DataBase.Tables;
using KinoApiCache.Utils;
using KinoTypes;
using KinoTypes.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiCache.CacheProvider
{
    internal class CachedMovies : IMovies
    {
        private readonly IMovies movies;
        private readonly CachedCalls interactor;

        public CachedMovies(IMovies movies, CachedCalls interactor)
        {
            this.movies = movies;
            this.interactor = interactor;
        }


        public async Task<MovieInfo> GetMovieByIdAsync(int id)
        {
            var cached = await interactor.GetResult<MovieInfo, MovieInfoDB>(nameof(GetMovieByIdAsync), id.ToString());
            if (cached != null)
                return cached.FirstOrDefault();

            var result = await movies.GetMovieByIdAsync(id);
            if (result == null)
                return null;
            await interactor.AddCall<MovieInfo, MovieInfoDB>(new[] { result }, nameof(GetMovieByIdAsync), id.ToString());
            return result;
        }

        public async Task<Movie[]> GetMoviesByKeywordAsync(string keyword, int page = 1)
        {
            var cached = await interactor.GetResult<Movie, MovieDB>(nameof(GetMoviesByKeywordAsync), keyword, page.ToString());
            if (cached != null)
                return cached;

            var result = await movies.GetMoviesByKeywordAsync(keyword, page);
            if (result == null)
                return Array.Empty<Movie>();
            await interactor.AddCall<Movie, MovieDB>(result, nameof(GetMoviesByKeywordAsync), keyword, page.ToString());
            return result;
        }
        public async Task<Movie[]> GetMovieByYearAsync(int year, Order order = Order.RATING, int page = 1)
        {
            var cached = await interactor.GetResult<Movie, MovieDB>(nameof(GetMovieByYearAsync),
                                                                     year.ToString(),
                                                                     order.ToString(),
                                                                     page.ToString());
            if (cached != null)
                return cached;

            var result = await movies.GetMovieByYearAsync(year, order, page);
            if (result == null)
                return Array.Empty<Movie>();
            await interactor.AddCall<Movie, MovieDB>(result,
                                                     nameof(GetMovieByYearAsync),
                                                     year.ToString(),
                                                     order.ToString(),
                                                     page.ToString());
            return result;
        }

        public async Task<Movie[]> GetMovieByGenreAsync(Genre genre, Order order = Order.RATING, int page = 1)
        {
            var cached = await interactor.GetResult<Movie, MovieDB>(nameof(GetMovieByGenreAsync),
                                                         genre.Id.ToString(),
                                                         order.ToString(),
                                                         page.ToString());
            if (cached != null)
                return cached;

            var result = await movies.GetMovieByGenreAsync(genre, order, page);
            if (result == null)
                return Array.Empty<Movie>();
            await interactor.AddCall<Movie, MovieDB>(result,
                                                     nameof(GetMovieByGenreAsync),
                                                     genre.Id.ToString(),
                                                     order.ToString(),
                                                     page.ToString());
            return result;
        }

        public async Task<MovieInfo[]> GetMovieByIdAsync(int[] ids)
        {
            var tasks = new Task<MovieInfo[]>[ids.Length];
            for (int i = 0; i < ids.Length; i++)
            {
                tasks[i] = interactor.GetResult<MovieInfo, MovieInfoDB>(nameof(GetMovieByIdAsync), ids[i].ToString());
            }
            var dbResult = (await Task.WhenAll(tasks)).Select(r => r?[0]).ToArray();
            var needToRequest = GetMissed(dbResult, ids);
            if (needToRequest.Length == 0)
                return dbResult;

            var requested = await movies.GetMovieByIdAsync(needToRequest);
            foreach (var movie in requested)
                await interactor.AddCall<MovieInfo, MovieInfoDB>(new[] { movie }, nameof(GetMovieByIdAsync), movie.KinopoiskId.ToString());

            var results = dbResult.Concat(requested);
            return ids.Select(id => results.Single(r => r != null && r.KinopoiskId == id)).ToArray();
        }


        private int[] GetMissed(MovieInfo[] movies, int[] ids)
        {
            var list = new List<int>();
            for (int i = 0; i < ids.Length; i++)
            {
                if (movies[i] == null)
                    list.Add(ids[i]);
            }
            return list.ToArray();
        }
    }
}
