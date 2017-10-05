using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TSC.Common.Models;
using TSC.Common.Services.Interfaces;
using Toolbox.Portable.Services;

namespace TSC.Common.Services
{
    public class ImageService : BaseHttpService, IImageService
    {
        HttpClient downloadClient = new HttpClient();

        public ImageService()
            : base("http://TSCfunctions.azurewebsites.net/api/functions/Image/")
        {

        }
        public async Task<string> GetBackgroundImageAsync(string city, string weather)
        {
            var searchWeather = weather.Replace(" ", "+");
            CityBackground cityBackground = Database.Connection.Table<CityBackground>()
                                                    .FirstOrDefault(cb => cb.Name == city && cb.WeatherOverview == searchWeather);

            if (cityBackground == null)
            {
                var remoteImage = await GetAsync<CityBackground>($"BackgroundByCityAndWeather?city={city}&weather={searchWeather}&height=1920");
                cityBackground = new CityBackground
                {
                    Name = city,
                    WeatherOverview = searchWeather,
                    ImageUrl = remoteImage.ImageUrl
                };
            }

            return await WebImageCache.RetrieveImage(cityBackground.ImageUrl, $"{city}-{searchWeather}");

        }
    }
}
