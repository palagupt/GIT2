using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Portable.Services;

namespace Toolbox.UWP.Services
{
    public class LocationService : ILocationService
    {
        public bool HasLocationPermission => throw new NotImplementedException();

        public Task<string> GetCityNameFromLocationAsync(LocationInfo location)
        {
            throw new NotImplementedException();
        }

        public Task<LocationInfo> GetCurrentLocationAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RequestPermissionAsync()
        {
            throw new NotImplementedException();
        }
    }
}
