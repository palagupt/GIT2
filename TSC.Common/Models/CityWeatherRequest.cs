using System;
namespace TSC.Common.Models
{
    public class CityWeatherRequest : WeatherRequest
    {
        public override string ToString()
        {
            return City?.Name;
        }

        public City City
        {
            get;
            set;
        }
    }
}
