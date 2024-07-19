using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public class MovieViewModel : ObservableObject, IDisposable
    {
        public bool IsFavorite
        {
            get => isFavorite;
            private set => SetProperty(ref isFavorite, value);
        }
        private bool isFavorite = false;

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

        public void Dispose()
        {
            if (isDisposed)
                return;

            FavoriteService.FavoriteChanged -= OnFavoriteChanged;
            isDisposed = true;
        }
    }
}
