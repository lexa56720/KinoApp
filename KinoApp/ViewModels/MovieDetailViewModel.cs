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
            set
            {
                SetProperty(ref movie, value);
            }
        }
        private MovieInfo movie;
        public MovieViewModel briefMovie
        {
            get => briefMovie1;
            set
            {
                SetProperty(ref briefMovie1, value);
            }
        }
        private MovieViewModel briefMovie1;

        private readonly MovieDetailModel model;
        public MovieDetailViewModel()
        {
            model = new MovieDetailModel(ApiService.Api);
        }

        public async Task InitAsync(MovieViewModel movie)
        {
            briefMovie = movie;
            Movie = await model.GetMovieInfo(movie.Movie);
        }
    }
}
