using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KinoApiWrapper.ResponseTypes.MoviesTypes.Keyword
{
    internal class MovieKeyword
    {
        [JsonPropertyName("filmId")]
        public int FilmId { get; set; }

        [JsonPropertyName("nameRu")]
        public string NameRu { get; set; }

        [JsonPropertyName("nameEn")]
        public string NameEn { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("year")]
        public string Year { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("filmLength")]
        public string FilmLength { get; set; }

        [JsonPropertyName("countries")]
        public List<Country> Countries { get; } = new List<Country>();

        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; } = new List<Genre>();

        [JsonPropertyName("rating")]
        public string Rating { get; set; }

        [JsonPropertyName("ratingVoteCount")]
        public int? RatingVoteCount { get; set; }

        [JsonPropertyName("posterUrl")]
        public string PosterUrl { get; set; }

        [JsonPropertyName("posterUrlPreview")]
        public string PosterUrlPreview { get; set; }
    }
}
