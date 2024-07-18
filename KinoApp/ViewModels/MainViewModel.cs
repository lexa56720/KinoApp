using CommunityToolkit.Mvvm.ComponentModel;
using KinoApp.Models;
using KinoApp.Services;
using KinoTypes;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace KinoApp.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public ObservableCollection<MovieViewModel> Movies { get; set; } = new ObservableCollection<MovieViewModel>();


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
        }
    }
}
