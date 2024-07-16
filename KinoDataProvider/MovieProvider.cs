using KinoTypes;
using KinoTypes.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoDataProvider
{
    internal class MovieProvider : IMovies
    {
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
