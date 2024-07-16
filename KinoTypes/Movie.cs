using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KinoTypes
{
    public class Movie
    {
        public int KinopoiskId { get; set; }

        public string KinopoiskHDId { get; set; }

        public string NameRu { get; set; }

        public string NameOriginal { get; set; }

        public string PosterUrl { get; set; }

        public string PosterUrlPreview { get; set; }

        public string CoverUrl { get; set; }

        public double RatingKinopoisk { get; set; }

        public int RatingKinopoiskVoteCount { get; set; }

        public double RatingImdb { get; set; }

        public int RatingImdbVoteCount { get; set; }

        public double RatingFilmCritics { get; set; }

        public int RatingFilmCriticsVoteCount { get; set; }

        public int Year { get; set; }

        public int FilmLength { get; set; }

        public string Slogan { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public string ProductionStatus { get; set; }

        public string Type { get; set; }

        public List<Country> Countries { get; } = new List<Country>();

        public List<Genre> Genres { get; } = new List<Genre>();

        public int StartYear { get; set; }

        public int EndYear { get; set; }

        public bool Serial { get; set; }

        public bool ShortFilm { get; set; }

        public bool Completed { get; set; }
    }
}
