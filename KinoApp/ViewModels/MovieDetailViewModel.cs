using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KinoApp.Models;
using KinoApp.Services;
using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        public ICommand FavoriteSwitchCommand
        {
            get
            {
                if (favoriteSwitchCommand == null)
                    favoriteSwitchCommand = new RelayCommand(FavoriteSwitch);
                return favoriteSwitchCommand;
            }
        }
        private ICommand favoriteSwitchCommand;

        public ICommand LoadCommand
        {
            get
            {
                if (loadCommand == null)
                    loadCommand = new RelayCommand(Loaded);
                return loadCommand;
            }
        }
        private ICommand loadCommand;

        public ICommand UnLoadCommand
        {
            get
            {
                if (unLoadCommand == null)
                    unLoadCommand = new RelayCommand(UnLoaded);
                return unLoadCommand;
            }
        }
        private ICommand unLoadCommand;


        private readonly MovieDetailModel model;
        public MovieDetailViewModel()
        {
            model = new MovieDetailModel(ApiService.Api);
        }

        public async Task InitAsync(MovieViewModel movie)
        {
            BriefMovie = movie.Movie;

            Movie = await model.GetMovieInfo(movie.Movie);
        }
        private void UnLoaded()
        {
            FavoriteService.FavoriteChanged -= OnFavoriteChanged;
        }

        private void Loaded()
        {
            IsFavorite = FavoriteService.IsContains(BriefMovie);
            FavoriteService.FavoriteChanged += OnFavoriteChanged;
        }

        private void OnFavoriteChanged(object sender, FavoriteChangedEventArgs e)
        {
            if (e.Id == BriefMovie.KinopoiskId && e.IsAdded)
                IsFavorite = true;
            else if (e.Id == BriefMovie.KinopoiskId && !e.IsAdded)
                IsFavorite = false;
        }

        private void FavoriteSwitch()
        {
            if (IsFavorite)
                FavoriteService.Remove(Movie);
            else
                FavoriteService.Add(Movie);
        }
    }
}
