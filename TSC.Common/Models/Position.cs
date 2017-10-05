﻿using Newtonsoft.Json;

namespace TSC.Common.Models
{
    public class Position
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; } = 0;

        [JsonProperty("lat")]
        public double Latitude { get; set; } = 0;
    }
}
