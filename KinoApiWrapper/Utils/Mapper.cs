﻿using KinoApiWrapper.ResponseTypes;
using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiWrapper.Utils
{
    internal class Mapper
    {
        public Movie Map(BriefMovieInfo response)
        {
            var movie = new Movie()
            {
                KinopoiskId = response.KinopoiskId, 
                NameRu = response.NameRu,
                NameOriginal = response.NameOriginal,
                PosterUrl = response.PosterUrl,
                PosterUrlPreview = response.PosterUrlPreview,
                RatingKinopoisk = response.RatingKinopoisk,
                RatingImdb = response.RatingImdb,
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
                NameRu = response.NameRu,
                NameOriginal = response.NameOriginal,
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
    }
}
