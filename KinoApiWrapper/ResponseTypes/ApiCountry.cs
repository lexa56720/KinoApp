﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KinoApiWrapper.ResponseTypes
{
    internal class ApiCountry
    {
        [JsonPropertyName("country")]
        public string Name { get; set; }
    }
}
