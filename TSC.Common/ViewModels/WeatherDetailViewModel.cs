using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TSC.Common.Models;
using TSC.Common.Services.Interfaces;
using Toolbox.Portable.Toolbox.Portable.Infrastructure;
using Toolbox.Portable.Mvvm;
using Toolbox.Portable.Services;

namespace TSC.Common.ViewModels
{
    public class WeatherDetailViewModel : BaseViewModel
    {
        private Task initTask;
        private string cityName;
        private Weather weather;
        private string backgroundPath;
        private WeatherRequest request;
        private IAlertService alertService = ServiceContainer.Resolve<IAlertService>();
        private ILocationService locationService = ServiceContainer.Resolve<ILocationService>();
        private IWeatherService weatherService = ServiceContainer.Resolve<IWeatherService>();
        private IImageService imageService = ServiceContainer.Resolve<IImageService>();

        public string TemperatureUnitString => string.IsNullOrWhiteSpace(this.Weather?.CurrentTemperature) ? string.Empty : "°F";

        public string Name => string.IsNullOrWhiteSpace(this.Weather?.Name) ? string.Empty : this.Weather.Name;

        public string LowTemperature => string.IsNullOrWhiteSpace(this.Weather?.MinTemperature) ? string.Empty : $"{this.Weather.MinTemperature} {this.TemperatureUnitString}";

        public string HighTemperature => string.IsNullOrWhiteSpace(this.Weather?.MaxTemperature) ? string.Empty : $"{this.Weather.MaxTemperature} {this.TemperatureUnitString}";

        public string Id => string.IsNullOrWhiteSpace(this.Weather?.Id) ? string.Empty : this.Weather.Id;

        public string Description => string.IsNullOrWhiteSpace(this.Weather?.Description) ? string.Empty : this.Weather.Description;

        public string Overview => string.IsNullOrWhiteSpace(this.Weather?.Overview) ? string.Empty : this.Weather.Overview;

        public string CurrentTemperatureSummary => string.IsNullOrWhiteSpace(this.Weather?.CurrentTemperature) ? string.Empty : this.Weather.CurrentTemperature;

        public WeatherRequest Request
        {
            get
            {
                return request;
            }

            set
            {
                request = value;
                InitAsync(null);
            }
        }

        public string CityName
        {
            get
            {
                return this.cityName;
            }

            set
            {
                RaiseAndUpdate(ref this.cityName, value);
            }
        }

        public Weather Weather
        {
            get
            {
                return this.weather;
            }
            set
            {
                if (RaiseAndUpdate(ref this.weather, value))
                    this.UpdateWeatherProperties();
            }
        }

        public string BackgroundPath
        {
            get
            {
                return this.backgroundPath;
            }

            set
            {
                RaiseAndUpdate(ref this.backgroundPath, value);
            }
        }

        public double CurrentTemperature
        {
            get
            {
                if (double.TryParse(Weather?.CurrentTemperature, out double result))
                    return result;
                return 0;
            }
        }

        public override Task InitAsync(Dictionary<string, object> data = null)
        {
            if (this.initTask?.IsCompleted ?? true)
            {
                this.initTask = this.InitTask(data);
            }

            return this.initTask;
        }

        private async Task InitTask(Dictionary<string, object> data = null)
        {
            try
            {
                if (this.Request == null)
                    return;

                string city = string.Empty;

                switch (this.Request)
                {
                    case CityWeatherRequest cwr:
                        city = cwr.City.Name;
                        break;
                    case LocationWeatherRequest lwr:
                        city = await locationService.GetCityNameFromLocationAsync(new LocationInfo(lwr.Position.Latitude, lwr.Position.Longitude));
                        break;
                    default:
                        break;
                }

                if (string.IsNullOrWhiteSpace(city))
                {
                    throw new NullReferenceException("City name cannot be null or whitespace");
                }

                this.CityName = city;
                this.Weather = await weatherService.GetWeatherAsync(city);
                this.BackgroundPath = await imageService.GetBackgroundImageAsync(this.CityName, this.Weather.Overview);
            }
            catch (WebException ex)
            {
                await this.alertService.DisplayAsync("TSC", ex.Message, "Done");
            }
        }

        private void UpdateWeatherProperties()
        {
            Raise(nameof(this.CurrentTemperature));
            Raise(nameof(this.CurrentTemperatureSummary));
            Raise(nameof(this.Name));
            Raise(nameof(this.LowTemperature));
            Raise(nameof(this.HighTemperature));
            Raise(nameof(this.Id));
            Raise(nameof(this.Description));
            Raise(nameof(this.Overview));
            Raise(nameof(this.TemperatureUnitString));
        }
    }
}