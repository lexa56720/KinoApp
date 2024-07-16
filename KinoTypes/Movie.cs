using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
    public class Movie
    {
        public int KinopoiskId { get; set; }
        public string NameRu { get; set; }

        public string NameOriginal { get; set; }

        public Country[] Countries { get; set; }

        public Genre[] Genres { get; set; }

        public double? RatingKinopoisk { get; set; }

        public double? RatingImdb { get; set; }

        public int? Year { get; set; }

        public MovieType Type { get; set; }

        public string PosterUrl { get; set; }

        public string PosterUrlPreview { get; set; }
    }
}
