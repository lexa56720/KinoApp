using KinoApp.Models;
using KinoApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApp.ViewModels
{
    public class FavoriteViewModel : BaseMovieListViewModel<FavoriteModel>
    {

        public FavoriteViewModel() : base()
        {
            FavoriteService.FavoriteChanged += FavoriteChanged;
        }

        private async void FavoriteChanged(object sender, FavoriteChangedEventArgs e)
        {
            if (e.IsAdded)
            {
                Movies.Insert(0, new MovieViewModel(await model.GetMovieAsync(e.Id)));
            }
            else
            {
                var movie = Movies.SingleOrDefault(m => m.Movie.KinopoiskId == e.Id);
                movie.Dispose();
                Movies.Remove(movie);
            }
        }

        protected override FavoriteModel CreateModel()
        {
            return new FavoriteModel(FavoriteService.List, ApiService.Api);
        }

        protected override async Task LoadMore()
        {
            var movies = await model.GetMoviesAsync(Movies.Select(m => m.Movie.KinopoiskId).ToArray());
            foreach (var movie in movies)
                if (!Movies.Any(m => m.Movie.KinopoiskId == movie.KinopoiskId))
                {
                    Movies.Add(new MovieViewModel(movie));
                }
        }
    }
}
