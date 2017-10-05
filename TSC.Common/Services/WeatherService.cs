using System.Threading.Tasks;
using TSC.Common.Models;
using TSC.Common.Services.Interfaces;
using Toolbox.Portable.Services;

namespace TSC.Common.Services
{
    public class WeatherService
        : BaseHttpService, IWeatherService
    {
        public WeatherService()
            : base("http://TSCfunctions.azurewebsites.net/api/functions/Weather/")
        { }

        public Task<Weather> GetWeatherAsync(double latitude, double longitude, TemperatureUnit unit = TemperatureUnit.Imperial)
        {
            return GetAsync<Weather>($"WeatherByPosition?latitude={latitude}&longitude={longitude}&unit={unit.ToString().ToLower()}");
        }

        public Task<Weather> GetWeatherAsync(string city, TemperatureUnit unit = TemperatureUnit.Imperial)
        {
            return GetAsync<Weather>($"WeatherByCity?city={city}&unit={unit.ToString().ToLower()}");
        }

        public Task<Weather> GetForecastAsync(int cityId, TemperatureUnit unit = TemperatureUnit.Imperial)
        {
            return GetAsync<Weather>($"ForecastByCityId?cityId={cityId}&units={unit.ToString().ToLower()}");
        }
    }
}
