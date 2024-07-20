using System.Text.Json.Serialization;

namespace KinoApiWrapper.ResponseTypes
{
    internal class ApiCountry
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("country")]
        public string Name { get; set; }
    }
}
