using KinoApiCache.DataBase.Tables.CachedType;

namespace KinoApiCache.DataBase.Tables
{
    public enum CachedMovieType
    {
        FILM,
        VIDEO,
        TV_SERIES,
        MINI_SERIES,
        TV_SHOW,
    }

    internal class MovieDB : ICachedEntity
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

        public int CallId { get; set; }

        public CallDB Call { get; set; }
    }
}
