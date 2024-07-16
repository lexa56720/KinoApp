﻿using KinoApiWrapper.ResponseTypes;
using KinoApiWrapper.ResponseTypes.MoviesTypes;
using KinoApiWrapper.ResponseTypes.MoviesTypes.Keyword;
using KinoApiWrapper.ResponseTypes.MoviesTypes.Search;
using KinoApiWrapper.Utils.Abstract;
using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiWrapper.Utils
{
    internal class Mapper : IMapper
    {
        public Movie Map(MovieSearch response)
        {
            var movie = new Movie()
            {
                KinopoiskId = response.KinopoiskId,
                Name = GetName(response.NameRu, response.NameOriginal, response.NameEn),
                PosterUrl = response.PosterUrl,
                PosterUrlPreview = response.PosterUrlPreview,
                RatingKinopoisk = response.RatingKinopoisk,
                Year = response.Year,
                Type = ParseType(response.Type),
                Countries = response.Countries?.Select(c => new Country { Id = c.Id, Name = c.Name }).ToArray(),
                Genres = response.Genres?.Select(g => new Genre { Id = g.Id, Name = g.Name }).ToArray(),
            };

            return movie;
        }

        public MovieInfo Map(FullMovieInfo response)
        {
            var movie = new MovieInfo()
            {
                KinopoiskId = response.KinopoiskId,
                KinopoiskHDId = response.KinopoiskHDId,
                Name = GetName(response.NameRu, response.NameOriginal, response.NameEn),
                PosterUrl = response.PosterUrl,
                PosterUrlPreview = response.PosterUrlPreview,
                CoverUrl = response.CoverUrl,
                RatingKinopoisk = response.RatingKinopoisk,
                RatingKinopoiskVoteCount = response.RatingKinopoiskVoteCount,
                RatingImdb = response.RatingImdb,
                RatingImdbVoteCount = response.RatingImdbVoteCount,
                RatingFilmCritics = response.RatingFilmCritics,
                RatingFilmCriticsVoteCount = response.RatingFilmCriticsVoteCount,
                Year = response.Year,
                FilmLength = response.FilmLength,
                Slogan = response.Slogan,
                Description = response.Description,
                ShortDescription = response.ShortDescription,
                ProductionStatus = response.ProductionStatus,
                Type = ParseType(response.Type),
                Countries = response.Countries?.Select(c => new Country { Id = c.Id, Name = c.Name }).ToArray(),
                Genres = response.Genres?.Select(g => new Genre { Id = g.Id, Name = g.Name }).ToArray(),
                StartYear = response.StartYear,
                EndYear = response.EndYear,
                Serial = response.Serial,
                ShortFilm = response.ShortFilm,
                Completed = response.Completed
            };

            return movie;
        }

        public Genre Map(ApiGenre response)
        {
            return new Genre
            {
                Id = response.Id,
                Name = response.Name
            };
        }

        public Movie Map(MovieKeyword response)
        {
            var movie = new Movie()
            {
                KinopoiskId = response.FilmId,
                Name = GetName(response.NameRu, response.NameEn),
                PosterUrl = response.PosterUrl,
                PosterUrlPreview = response.PosterUrlPreview,
                RatingKinopoisk = TryParseRating(response.Rating),
                Year = int.Parse(response.Year),
                Type = ParseType(response.Type),
                Countries = response.Countries?.Select(c => new Country { Id = c.Id, Name = c.Name }).ToArray(),
                Genres = response.Genres?.Select(g => new Genre { Id = g.Id, Name = g.Name }).ToArray(),
            };
            return movie;
        }

        private double? TryParseRating(string value)
        {
            if (double.TryParse(value, out var res))
                return res;
            return null;
        }

        private MovieType ParseType(string type)
        {
            switch (type)
            {
                case "FILM": return MovieType.FILM;
                case "VIDEO": return MovieType.VIDEO;
                case "TV_SERIES": return MovieType.TV_SERIES;
                case "MINI_SERIES": return MovieType.MINI_SERIES;
                case "TV_SHOW": return MovieType.TV_SHOW;
            }
            return default;
        }

        private string GetName(params string[] names)
        {
            foreach (var name in names)
                if (!string.IsNullOrEmpty(name))
                    return name;
            return string.Empty;
        }
    }
}
