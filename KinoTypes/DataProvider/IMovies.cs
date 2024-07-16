using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoTypes.DataProvider
{
    public interface IMovies
    {
        Task<MovieInfo> GetMovieByIdAsync(int id);

        Task<Movie[]> GetMovieByYearAsync(int year, Order order = Order.RATING, int page = 1);

        Task<Movie[]> GetMovieByGenreAsync(Genre genre, Order order = Order.RATING, int page = 1);

        Task<Movie[]> GetMoviesByKeywordAsync(string keyword, int page = 1);
    }
}
