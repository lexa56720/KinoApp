using CommunityToolkit.Mvvm.ComponentModel;
using KinoApp.Services;
using KinoTypes;
using System;
using Windows.UI.Xaml.Controls;

namespace KinoApp.ViewModels
{
    public class MovieViewModel : ObservableObject, IDisposable
    {
        public bool IsFavorite
        {
            get => isFavorite;
            private set
            {
                SetProperty(ref isFavorite, value);
                if (value)
                    Symbol = Symbol.Favorite;
                else
                    Symbol = Symbol.OutlineStar;
            }

        }
        private bool isFavorite = false;

        public Symbol Symbol
        {
            get => symbol;
            private set => SetProperty(ref symbol, value);
        }
        private Symbol symbol = Symbol.OutlineStar;

        public Movie Movie { get; }
        private bool isDisposed = false;
        public MovieViewModel(Movie movie)
        {
            Movie = movie;
            IsFavorite = FavoriteService.IsContains(movie);
            FavoriteService.FavoriteChanged += OnFavoriteChanged;
        }

        private void OnFavoriteChanged(object sender, FavoriteChangedEventArgs e)
        {
            if (e.Id == Movie.KinopoiskId && e.IsAdded)
                IsFavorite = true;
            else if (e.Id == Movie.KinopoiskId && !e.IsAdded)
                IsFavorite = false;
        }

        public void Update()
        {
            OnPropertyChanged(nameof(IsFavorite));
        }

        public void Dispose()
        {
            if (isDisposed)
                return;

            FavoriteService.FavoriteChanged -= OnFavoriteChanged;
            isDisposed = true;
        }
    }
}
