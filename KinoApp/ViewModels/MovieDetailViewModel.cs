using CommunityToolkit.Mvvm.ComponentModel;
using KinoApp.Models;
using KinoApp.Services;
using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApp.ViewModels
{
    public class MovieDetailViewModel : ObservableObject
    {
        public MovieInfo Movie
        {
            get => movie;
            private set => SetProperty(ref movie, value);
        }
        private MovieInfo movie;

        public Movie BriefMovie
        {
            get => briefMovie;
            set
            {
                SetProperty(ref briefMovie, value);
            }
        }
        private Movie briefMovie;

        public bool IsFavorite
        {
            get => isFavorite;
            private set => SetProperty(ref isFavorite, value);
        }
        private bool isFavorite = false;

        private readonly MovieDetailModel model;
        public MovieDetailViewModel()
        {
            model = new MovieDetailModel(ApiService.Api);
        }

        public async Task InitAsync(MovieViewModel movie)
        {
            BriefMovie = movie.Movie;
            IsFavorite = FavoriteService.IsContains(BriefMovie);
            FavoriteService.FavoriteChanged += OnFavoriteChanged;
            Movie = await model.GetMovieInfo(movie.Movie);
        }

        private void OnFavoriteChanged(object sender, FavoriteChangedEventArgs e)
        {
            if (e.Id == BriefMovie.KinopoiskId && e.IsAdded)
                IsFavorite = true;
            else if (e.Id == BriefMovie.KinopoiskId && !e.IsAdded)
                IsFavorite = false;
        }
    }
}
