using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KinoApp.Models;
using KinoApp.Services;
using KinoTypes;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;


namespace KinoApp.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public ObservableCollection<MovieViewModel> Movies { get; set; } = new ObservableCollection<MovieViewModel>();

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
                    loadMoreCommand = new AsyncRelayCommand(LoadMore, () => !IsFullyLoaded);
                return loadMoreCommand;
            }
        }
        private IAsyncRelayCommand loadMoreCommand;


        private bool IsFullyLoaded;
        private readonly MainModel model;
        public MainViewModel()
        {
            model = new MainModel(ApiService.Api);
            LoadMore();
        }

        private async Task LoadMore()
        {
            var movies = await model.GetMoviesAsync();
            foreach (var movie in movies)
                Movies.Add(new MovieViewModel(movie));
            IsFullyLoaded = movies.Length == 0;
        }

        private void OpenMovie(MovieViewModel movie)
        {
            throw new NotImplementedException();
        }
    }
}
