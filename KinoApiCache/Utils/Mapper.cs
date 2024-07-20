using KinoApiCache.DataBase.Tables;
using KinoApiCache.DataBase.Tables.CachedType;
using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiCache.Utils
{
    internal class Mapper : IMapper
    {
        public TKino Map<TKino, TDb>(TDb item)
            where TDb : class, ICachedEntity
            where TKino : class
        {
            switch (item)
            {
                case MovieInfoDB movieInfo:
                    return Map(movieInfo) as TKino;

                case MovieDB movie:
                    return Map(movie) as TKino;

                case GenreDB genre:
                    return Map(genre) as TKino;
            }
            return null;
        }
        public TDb ReverseMap<TKino, TDb>(TKino item)
                     where TDb : class, ICachedEntity
                     where TKino : class
        {
            switch (item)
            {
                case MovieInfo movieInfo:
                    return Map(movieInfo) as TDb;

                case Movie movie:
                    return Map(movie) as TDb;

                case Genre genre:
                    return Map(genre) as TDb;
            }
            return null;
        }

        private Movie Map(MovieDB movieDB)
        {
            return new Movie()
            {
                KinopoiskId = movieDB.KinopoiskId,
                Name = movieDB.Name,
                Countries = MapCounries(movieDB.Countries),
                Genres = MapGenres(movieDB.Genres),
                RatingKinopoisk = movieDB.RatingKinopoisk,
                Year = movieDB.Year,
                Type = Map(movieDB.Type),
                PosterUrl = movieDB.PosterUrl,
                PosterUrlPreview = movieDB.PosterUrlPreview
            };
        }
        private MovieDB Map(Movie movie)
        {
            return new MovieDB()
            {
                KinopoiskId = movie.KinopoiskId,
                Name = movie.Name,
                Countries = MapCounries(movie.Countries),
                Genres = MapGenres(movie.Genres),
                RatingKinopoisk = movie.RatingKinopoisk,
                Year = movie.Year,
                Type = Map(movie.Type),
                PosterUrl = movie.PosterUrl,
                PosterUrlPreview = movie.PosterUrlPreview
            };
        }

        private MovieInfo Map(MovieInfoDB movieDB)
        {
            return new MovieInfo()
            {
                KinopoiskId = movieDB.KinopoiskId,
                Name = movieDB.Name,
                Countries = MapCounries(movieDB.Countries),
                Genres = MapGenres(movieDB.Genres),
                RatingKinopoisk = movieDB.RatingKinopoisk,
                Year = movieDB.Year,
                Type = Map(movieDB.Type),
                PosterUrl = movieDB.PosterUrl,
                PosterUrlPreview = movieDB.PosterUrlPreview,
                KinopoiskHDId = movieDB.KinopoiskHDId,
                CoverUrl = movieDB.CoverUrl,
                RatingKinopoiskVoteCount = movieDB.RatingKinopoiskVoteCount,
                RatingImdb = movieDB.RatingImdb,
                RatingImdbVoteCount = movieDB.RatingImdbVoteCount,
                RatingFilmCritics = movieDB.RatingFilmCritics,
                RatingFilmCriticsVoteCount = movieDB.RatingFilmCriticsVoteCount,
                FilmLength = movieDB.FilmLength,
                Slogan = movieDB.Slogan,
                Description = movieDB.Description,
                ShortDescription = movieDB.ShortDescription,
                ProductionStatus = movieDB.ProductionStatus,
                StartYear = movieDB.StartYear,
                EndYear = movieDB.EndYear,
                Serial = movieDB.Serial,
                ShortFilm = movieDB.ShortFilm,
                Completed = movieDB.Completed
            };
        }
        private MovieInfoDB Map(MovieInfo movie)
        {
            return new MovieInfoDB()
            {
                KinopoiskId = movie.KinopoiskId,
                Name = movie.Name,
                Countries = MapCounries(movie.Countries),
                Genres = MapGenres(movie.Genres),
                RatingKinopoisk = movie.RatingKinopoisk,
                Year = movie.Year,
                Type = Map(movie.Type),
                PosterUrl = movie.PosterUrl,
                PosterUrlPreview = movie.PosterUrlPreview,
                KinopoiskHDId = movie.KinopoiskHDId,
                CoverUrl = movie.CoverUrl,
                RatingKinopoiskVoteCount = movie.RatingKinopoiskVoteCount,
                RatingImdb = movie.RatingImdb,
                RatingImdbVoteCount = movie.RatingImdbVoteCount,
                RatingFilmCritics = movie.RatingFilmCritics,
                RatingFilmCriticsVoteCount = movie.RatingFilmCriticsVoteCount,
                FilmLength = movie.FilmLength,
                Slogan = movie.Slogan,
                Description = movie.Description,
                ShortDescription = movie.ShortDescription,
                ProductionStatus = movie.ProductionStatus,
                StartYear = movie.StartYear,
                EndYear = movie.EndYear,
                Serial = movie.Serial,
                ShortFilm = movie.ShortFilm,
                Completed = movie.Completed
            };
        }

        private Genre Map(GenreDB genreDB)
        {
            return new Genre()
            {
                Id = genreDB.GenreId,
                Name = genreDB.Name,
            };
        }
        private GenreDB Map(Genre genre)
        {
            return new GenreDB()
            {
                GenreId = genre.Id,
                Name = genre.Name,
            };
        }

        private string MapCounries(Country[] countries)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < countries.Length; i++)
            {
                sb.Append(countries[i].Id);
                sb.Append("_");
                sb.Append(countries[i].Name);
                sb.Append("|");
            }
            return sb.ToString();
        }
        private Country[] MapCounries(string countries)
        {
            var pairs = countries.Split("|",StringSplitOptions.RemoveEmptyEntries);
            var result = new Country[pairs.Length];

            for (int i = 0; i < pairs.Length; i++)
            {
                var values = pairs[i].Split("_", StringSplitOptions.RemoveEmptyEntries);
                result[i] = new Country()
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                };
            }
            return result;
        }

        private string MapGenres(Genre[] genres)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < genres.Length; i++)
            {
                sb.Append(genres[i].Id);
                sb.Append("_");
                sb.Append(genres[i].Name);
                sb.Append("|");
            }
            return sb.ToString();
        }
        private Genre[] MapGenres(string genres)
        {
            var pairs = genres.Split("|", StringSplitOptions.RemoveEmptyEntries);
            var result = new Genre[pairs.Length];

            for (int i = 0; i < pairs.Length; i++)
            {
                var values = pairs[i].Split("_", StringSplitOptions.RemoveEmptyEntries);
                result[i] = new Genre()
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                };
            }
            return result;
        }

        private MovieType Map(CachedMovieType movieType)
        {
            return (MovieType)movieType;
        }
        private CachedMovieType Map(MovieType movieType)
        {
            return (CachedMovieType)movieType;
        }
    }
}
