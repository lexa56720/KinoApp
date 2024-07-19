using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KinoApp.Models;
using KinoApp.Services;
using KinoApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KinoApp.ViewModels
{
    public abstract class BaseMovieListViewModel<T> : ObservableObject where T : BaseMovieListModel
    {
        public ObservableCollection<MovieViewModel> Movies { get; set; }
                = new ObservableCollection<MovieViewModel>();

        public ICommand OpenMovieCommand
        {
            get
            {
                if (openMovieCommand == null)
                    openMovieCommand = new RelayCommand<MovieViewModel>(OpenMovie);
                return openMovieCommand;
            }
        }
        private ICommand openMovieCommand;

        public IAsyncRelayCommand LoadMoreCommand
        {
            get
            {
                if (loadMoreCommand == null)
                    loadMoreCommand = new AsyncRelayCommand(LoadMore);
                return loadMoreCommand;
            }
        }
        private IAsyncRelayCommand loadMoreCommand;

        protected readonly T model;
        public BaseMovieListViewModel()
        {
            model = CreateModel();
        }

        protected abstract T CreateModel();

        protected virtual async Task LoadMore()
        {
            var movies = await model.GetMoviesAsync();
            foreach (var movie in movies)
                if (!Movies.Any(m => m.Movie.KinopoiskId == movie.KinopoiskId))
                    Movies.Add(new MovieViewModel(movie));
        }

        private void OpenMovie(MovieViewModel movie)
        {
            NavigationService.Navigate(typeof(MovieDetailPage), movie);
        }

        internal async Task InitAsync()
        {
            if (Movies.Count == 0)
                await LoadMore();
        }
    }
}
