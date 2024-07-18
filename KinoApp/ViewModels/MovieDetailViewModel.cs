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
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
            }
        }
        private string name;

        public Country[] Countries
        {
            get => countries;
            set
            {
                SetProperty(ref countries, value);
            }
        }
        private Country[] countries = Array.Empty<Country>();

        public Genre[] Genres
        {
            get => genres;
            set
            {
                SetProperty(ref genres, value);
            }
        }
        private Genre[] genres=Array.Empty<Genre>();

        public int Year
        {
            get => year;
            set
            {
                year = value;
                OnPropertyChanged(nameof(Year));
            }
        }
        private int year;

        public MovieType Type
        {
            get => type;
            set
            {
                SetProperty(ref type, value);
            }
        }
        private MovieType type;

        public string PosterUrl
        {
            get => posterUrl;
            set
            {
                SetProperty(ref posterUrl, value);
            }
        }
        private string posterUrl;

        public double RatingKinopoisk
        {
            get => ratingKinopoisk;
            set
            {
                SetProperty(ref ratingKinopoisk, value);
            }
        }
        private double ratingKinopoisk;

        public int RatingKinopoiskVoteCount
        {
            get => ratingKinopoiskVoteCount;
            set
            {
                SetProperty(ref ratingKinopoiskVoteCount, value);
            }
        }
        private int ratingKinopoiskVoteCount;

        public double RatingImdb
        {
            get => ratingImdb;
            set
            {
                SetProperty(ref ratingImdb, value);
            }
        }
        private double ratingImdb;

        public int RatingImdbVoteCount
        {
            get => ratingImdbVoteCount;
            set
            {
                SetProperty(ref ratingImdbVoteCount, value);
            }
        }
        private int ratingImdbVoteCount;

        public int FilmLength
        {
            get => filmLength;
            set
            {
                SetProperty(ref filmLength, value);
            }
        }
        private int filmLength;

        public string Slogan
        {
            get => slogan;
            set
            {
                SetProperty(ref slogan, value);
            }
        }
        private string slogan;

        public string Description
        {
            get => description;
            set
            {
                SetProperty(ref description, value);
            }
        }
        private string description;

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
            Assign(await model.GetMovieInfo(movie.Movie));
        }

        private void Assign(MovieInfo movie)
        {
            Name = movie.Name;
            Countries = movie.Countries;
            Genres = movie.Genres;
            Year = movie.Year.GetValueOrDefault();
            Type = movie.Type;
            PosterUrl = movie.PosterUrl;
            RatingKinopoisk = movie.RatingKinopoisk.GetValueOrDefault();
            RatingKinopoiskVoteCount = movie.RatingKinopoiskVoteCount.GetValueOrDefault();
            RatingImdb = movie.RatingImdb.GetValueOrDefault();
            RatingImdbVoteCount = movie.RatingImdbVoteCount.GetValueOrDefault();
            FilmLength = movie.FilmLength.GetValueOrDefault();
            Slogan = movie.Slogan;
            Description = movie.Description;
        }
    }
}
