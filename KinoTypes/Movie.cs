namespace KinoTypes
{
    public enum MovieType
    {
        FILM,
        VIDEO,
        TV_SERIES,
        MINI_SERIES,
        TV_SHOW,
    }

    public enum Order
    {
        RATING,
        NUM_VOTE,
        YEAR
    }
    public class Movie
    {
        public int KinopoiskId { get; set; }
        public string Name { get; set; }

        public Country[] Countries { get; set; }

        public Genre[] Genres { get; set; }

        public double? RatingKinopoisk { get; set; }

        public int? Year { get; set; }

        public MovieType Type { get; set; }

        public string PosterUrl { get; set; }

        public string PosterUrlPreview { get; set; }
    }
}
