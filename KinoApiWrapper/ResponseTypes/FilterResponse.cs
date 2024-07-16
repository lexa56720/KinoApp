using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
