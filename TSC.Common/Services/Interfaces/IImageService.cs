using System;
using System.Threading.Tasks;
using TSC.Common.Models;

namespace TSC.Common.Services.Interfaces
{
    public interface IImageService
    {
        Task<string> GetBackgroundImageAsync(string city, string weather);
    }
}
