using System.Text.Json.Serialization;

namespace KinoApiWrapper.ResponseTypes
{
    internal class ApiGenre
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("genre")]
        public string Name { get; set; }
    }
}
