using KinoApiCache.DataBase.Tables.CachedType;

namespace KinoApiCache.DataBase.Tables
{
    internal class MovieInfoDB : ICachedEntity
    {
        public int Id { get; set; }

        public int KinopoiskId { get; set; }
        public string Name { get; set; }

        public string Countries { get; set; }

        public string Genres { get; set; }

        public double? RatingKinopoisk { get; set; }

        public int? Year { get; set; }

        public CachedMovieType Type { get; set; }

        public string PosterUrl { get; set; }

        public string PosterUrlPreview { get; set; }

        public string KinopoiskHDId { get; set; }

        public string CoverUrl { get; set; }

        public int? RatingKinopoiskVoteCount { get; set; }

        public double? RatingImdb { get; set; }

        public int? RatingImdbVoteCount { get; set; }

        public double? RatingFilmCritics { get; set; }

        public int? RatingFilmCriticsVoteCount { get; set; }

        public int? FilmLength { get; set; }

        public string Slogan { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public string ProductionStatus { get; set; }

        public int? StartYear { get; set; }

        public int? EndYear { get; set; }

        public bool? Serial { get; set; }

        public bool? ShortFilm { get; set; }

        public bool? Completed { get; set; }

        public int CallId { get; set; }

        public CallDB Call { get; set; }
    }
}
