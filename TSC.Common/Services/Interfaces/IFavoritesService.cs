using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TSC.Common.Models;

namespace TSC.Common.Services.Interfaces
{
    public interface IFavoritesService
    {
        Task<List<City>> GetFavoriteCitiesAsync();
        Task<City> AddFavoriteCityAsync(City city);
    }
}