using System.Text.Json.Serialization;

namespace KinoApiWrapper.ResponseTypes.MoviesTypes.Search
{
    internal class MovieSearch
    {
        [JsonPropertyName("kinopoiskId")]
        public int KinopoiskId { get; set; }

        [JsonPropertyName("imdbId")]
        public string ImdbId { get; set; }

        [JsonPropertyName("nameRu")]
        public string NameRu { get; set; }

        [JsonPropertyName("nameEn")]
        public string NameEn { get; set; }

        [JsonPropertyName("nameOriginal")]
        public string NameOriginal { get; set; }

        [JsonPropertyName("countries")]
        public ApiCountry[] Countries { get; set; }

        [JsonPropertyName("genres")]
        public ApiGenre[] Genres { get; set; }

        [JsonPropertyName("ratingKinopoisk")]
        public double? RatingKinopoisk { get; set; }

        [JsonPropertyName("ratingImdb")]
        public double? RatingImdb { get; set; }

        [JsonPropertyName("year")]
        public int? Year { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("posterUrl")]
        public string PosterUrl { get; set; }

        [JsonPropertyName("posterUrlPreview")]
        public string PosterUrlPreview { get; set; }
    }
}
