using System.Threading.Tasks;

namespace KinoTypes.DataProvider
{
    public interface IMovies
    {
        Task<MovieInfo> GetMovieByIdAsync(int id);

        Task<MovieInfo[]> GetMovieByIdAsync(int[] ids);

        Task<Movie[]> GetBestMoviesAsync(int page);

        Task<Movie[]> GetMoviesFilteredAsync(int? yearFrom, int? yearTo, Genre genre, Order? order, string keyword, int page);
    }
}
