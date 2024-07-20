using System.Text.Json.Serialization;

namespace KinoApiWrapper.ResponseTypes.MoviesTypes.Search
{
    internal class MovieSearchResponse
    {
        [JsonPropertyName("total")]
        public int? Total { get; set; }

        [JsonPropertyName("totalPages")]
        public int? TotalPages { get; set; }

        [JsonPropertyName("items")]
        public MovieSearch[] Items { get; set; }
    }
}
