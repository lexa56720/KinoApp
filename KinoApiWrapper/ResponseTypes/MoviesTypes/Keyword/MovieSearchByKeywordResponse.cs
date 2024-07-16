using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KinoApiWrapper.ResponseTypes.MoviesTypes.Keyword
{
    internal class MovieSearchByKeywordResponse
    {
        [JsonPropertyName("keyword")]
        public string Keyword { get; set; }

        [JsonPropertyName("pagesCount")]
        public int? PagesCount { get; set; }

        [JsonPropertyName("films")]
        public MovieKeyword[] Films { get; set; }

        [JsonPropertyName("searchFilmsCountResult")]
        public int? SearchFilmsCountResult { get; set; }
    }
}
