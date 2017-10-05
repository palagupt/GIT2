using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Toolbox.MVVM.Input;
using Toolbox.Portable.Mvvm;
using Toolbox.Portable.Services;
using Toolbox.Portable.Toolbox.Portable.Infrastructure;
using Toolbox.Portable.Utilities;
using TSC.Common.Models;
using TSC.Common.Services.Interfaces;

namespace TSC.Common.ViewModels
{
    public class WeatherListViewModel : BaseViewModel
    {
        private readonly SynchronizationContext _syncContext;
        private IFavoritesService favoritesService = ServiceContainer.Resolve<IFavoritesService>();
        private ILocationService locationService = ServiceContainer.Resolve<ILocationService>();
        private IAlertService alertService = ServiceContainer.Resolve<IAlertService>();
        private IWeatherService weatherService = ServiceContainer.Resolve<IWeatherService>();
        private ICategoryService categoryService = ServiceContainer.Resolve<ICategoryService>();
        private ObservableCollection<WeatherRequest> weatherRequests = new ObservableCollection<WeatherRequest>();
        private ObservableCollection<CategoryView_CategoryDetails.CatalogGroupView> categoryRequests = new ObservableCollection<CategoryView_CategoryDetails.CatalogGroupView>();
        private INavigationService navigationService = ServiceContainer.Resolve<INavigationService>();

        public WeatherListViewModel()
        {
            _syncContext = SynchronizationContext.Current;
        }

        public Action ReloadAction { get; set; }

        public AsyncCommand AddFavoriteCommand => new AsyncCommand(this.AddFavorite);

        public ICommand GoNextPageCommand => new Command(this.GoNextPage);

        private void GoNextPage(object arg)
        {
            navigationService.Navigate(PageTokens.MainPage);
        }

        public ObservableCollection<WeatherRequest> WeatherRequests
        {
            get { return this.weatherRequests; }
            set { this.RaiseAndUpdate(ref this.weatherRequests, value); }
        }

        public ObservableCollection<CategoryView_CategoryDetails.CatalogGroupView> CategoryRequests
        {
            get { return this.categoryRequests; }
            set { this.RaiseAndUpdate(ref this.categoryRequests, value); }
        }

        public override async Task InitAsync(Dictionary<string, object> data = null)
        {
            //var currentLocationTask = locationService.GetCurrentLocationAsync();
            var favoriteCitiesTask = favoritesService.GetFavoriteCitiesAsync();

            var categoriesTask = categoryService.GetCategoryAsync();
            await Task.WhenAll(categoriesTask).ConfigureAwait(false);
            var categories = categoriesTask.Result;

            var requests = new ObservableCollection<CategoryView_CategoryDetails.CatalogGroupView>
                (categories.CatalogGroupViewProperty);

            _syncContext.Post((param) => { this.CategoryRequests = requests; }, null);
            //await Task.WhenAll(currentLocationTask, favoriteCitiesTask).ConfigureAwait(false);
            // await Task.WhenAll(favoriteCitiesTask).ConfigureAwait(false);

            //var currentLocation = currentLocationTask.Result;
            // var favoriteCities = favoriteCitiesTask.Result;

            //var requests = new ObservableCollection<WeatherRequest>(favoriteCities.Select(c => new CityWeatherRequest
            //{
            //    City = c
            //}));

            //if (currentLocation != null)
            //{
            //    requests.Insert(0, new LocationWeatherRequest(new Position { Latitude = currentLocation.Latitude, Longitude = currentLocation.Longitude }));
            //}

          //  _syncContext.Post((param) => { this.WeatherRequests = requests; }, null);

            ReloadAction?.Invoke();
        }

        private async Task AddFavorite(object arg)
        {
            var favoriteText = await this.alertService.DisplayInputEntryAsync("TSC", "Enter city name", validator: (text) =>
            {
                return this.WeatherRequests?.Where((WeatherRequest i) => (i as CityWeatherRequest)?.City?.Name == text).Count() == 0;
            });

            if (!string.IsNullOrWhiteSpace(favoriteText))
            {
                Weather weather = null;

                try
                {
                    weather = await this.weatherService.GetWeatherAsync(favoriteText);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    if (weather == null)
                    {
                        await this.alertService.DisplayAsync("TSC", $"Cannot find weather data for {favoriteText}", "OK");
                    }
                    else
                    {
                        var favoriteCity = await this.favoritesService.AddFavoriteCityAsync(new City { Name = favoriteText });
                        this.WeatherRequests.Add(new CityWeatherRequest { City = new City { Name = favoriteText } });
                        ReloadAction?.Invoke();
                    }
                }
            }
        }
    }
}
