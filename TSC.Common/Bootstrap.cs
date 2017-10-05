using System;
using Toolbox.Portable.Toolbox.Portable.Infrastructure;
using TSC.Common.Services;
using SQLite;
using Toolbox.Portable.Services;
using System.IO;
using TSC.Common.Models;
using TSC.Common.Services.Interfaces;

namespace TSC.Common
{
    public static class Bootstrap
    {
        public static void Begin(Func<ILocationService> locationService, 
            Func<IAlertService> alertService, Action platformSpecificBegin)
        {
            ServiceContainer.Register<ILocationService>(locationService);
            ServiceContainer.Register<IAlertService>(alertService);
            ServiceContainer.Register<IImageService>(() => new ImageService());
            ServiceContainer.Register<IFavoritesService>(() => new FavoritesService());
            ServiceContainer.Register<IWeatherService>(() => new WeatherService());
            ServiceContainer.Register<ICategoryService>(() => new CategoryService());
            platformSpecificBegin();
        }
    }
}