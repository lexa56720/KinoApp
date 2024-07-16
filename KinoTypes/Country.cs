using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KinoTypes
{
    public class Country
    {
        public int Id { get; set; }

        [JsonPropertyName("country")]
        public string Name { get; set; }
    }
}
