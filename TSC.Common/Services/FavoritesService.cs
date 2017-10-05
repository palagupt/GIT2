using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSC.Common.Models;
using TSC.Common.Services.Interfaces;
using Toolbox.Portable.Services;

namespace TSC.Common.Services
{
    public class FavoritesService : IFavoritesService
    {
        public Task<List<City>> GetFavoriteCitiesAsync()
        {
            return Task.Run(() =>
            {
                var cities = Database.Connection.Table<City>().ToList();
                return cities;
            });
        }

        public Task<City> AddFavoriteCityAsync(City city)
        {
            if (string.IsNullOrWhiteSpace(city?.Name))
            {
                throw new ArgumentException("City and City Name cannot be null or whitespace.");
            }

			return Task.Run(() =>
			{
				var cityTable = Database.Connection.Table<City>();

				if (cityTable != null)
				{
					Database.Connection.Insert(city);
				}
                else
                {
                    throw new Exception("Database not initialized correctly.");
                }

                return city;
			});
        }
    }
}