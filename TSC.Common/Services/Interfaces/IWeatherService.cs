using System;
using System.Threading.Tasks;
using TSC.Common.Models;

namespace TSC.Common.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<Weather> GetWeatherAsync(double latitude, double longitude, TemperatureUnit unit = TemperatureUnit.Imperial);

        Task<Weather> GetWeatherAsync(string city, TemperatureUnit unit = TemperatureUnit.Imperial);

        //Task<Forecast> GetForecastAsync(int id, TemperatureUnit unit = TemperatureUnit.Imperial);
    }
}
