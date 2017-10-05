using System;
namespace TSC.Common.Models
{
    public class LocationWeatherRequest
        : WeatherRequest
    {
        public LocationWeatherRequest()
        {
        }

        public override string ToString()
        {
            return "Current Location";
        }

        public LocationWeatherRequest(Position position)
        {
            this.Position = position;
        }

        public Position Position
        {
            get;
            set;
        }
    }
}