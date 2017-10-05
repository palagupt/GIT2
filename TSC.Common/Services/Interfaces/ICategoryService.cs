using System;
using System.Threading.Tasks;
using TSC.Common.Models;

namespace TSC.Common.Services.Interfaces
{
   public interface ICategoryService
    {
        Task<CategoryView_CategoryDetails> GetCategoryAsync();
    }
}
