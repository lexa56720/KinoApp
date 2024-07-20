using System.Text.Json.Serialization;

namespace KinoApiWrapper.ResponseTypes
{
    internal class FilterResponse
    {
        [JsonPropertyName("genres")]
        public ApiGenre[] Genres { get; set; }

        [JsonPropertyName("countries")]
        public ApiCountry[] Countries { get; set; }
    }
}
