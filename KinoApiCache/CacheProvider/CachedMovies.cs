using KinoApiCache.DataBase;
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
        private readonly int itemsPerPage;
        private readonly DbContextFactory factory;
        private readonly IMapper mapper;

        public CachedMovies(IMovies movies,DbContextFactory factory,IMapper mapper, int itemsPerPage)
        {
            this.movies = movies;
            this.itemsPerPage = itemsPerPage;
            this.factory = factory;
            this.mapper = mapper;
        }
        public Task<Movie[]> GetMovieByGenreAsync(Genre genre, Order order = Order.RATING, int page = 1)
        {
            throw new NotImplementedException();
        }

        public Task<MovieInfo> GetMovieByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Movie[]> GetMovieByYearAsync(int year, Order order = Order.RATING, int page = 1)
        {
            throw new NotImplementedException();
        }

        public Task<Movie[]> GetMoviesByKeywordAsync(string keyword, int page = 1)
        {
            throw new NotImplementedException();
        }
    }
}
