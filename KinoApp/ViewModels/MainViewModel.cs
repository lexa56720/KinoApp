using KinoApp.Models;
using KinoApp.Services;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Threading.Tasks;


namespace KinoApp.ViewModels
{
    public class MainViewModel : BaseMovieListViewModel<MainModel>
    {
        protected override MainModel CreateModel()
        {
            return new MainModel(ApiService.Api);
        }

        protected override async Task LoadMore()
        {
            var movies = await model.GetMoviesAsync();
            foreach (var movie in movies)
                if (!Movies.Any(m => m.Movie.KinopoiskId == movie.KinopoiskId))
                    Movies.Add(new MovieViewModel(movie));
        }
    }
}
