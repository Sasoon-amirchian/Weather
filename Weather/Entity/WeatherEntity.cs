using System;
using System.Collections.Generic;

namespace Weather
{
    public record RootData
    {
        public List<WeatherData> data { get; init; }
        public List<Minutely> minutely { get; init; }
    }

    public record WeatherData
    {
        public string timezone { get; init; }
        public string state_code { get; init; }
        public string city_name { get; init; }
        public int precip { get; init; }
        public Weather weather { get; init; }
        public decimal temp { get; init; }
    }

    public class Minutely
    {
        public DateTime timestamp_utc { get; set; }
        public int snow { get; set; }
        public decimal temp { get; set; }
        public DateTime timestamp_local { get; set; }
        public int ts { get; set; }
        public int precip { get; set; }
    }

    public record Weather
    {
        public string description { get; set; }
    }
}