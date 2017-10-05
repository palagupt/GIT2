using System.Threading.Tasks;
using TSC.Common.Models;
using TSC.Common.Services.Interfaces;
using Toolbox.Portable.Services;
using System;

namespace TSC.Common.Services
{
    public class CategoryService : BaseHttpService, ICategoryService
    {
        public CategoryService()
            : base("http://localhost:80/wcs/resources/store/11001/")
        { }

        public Task<CategoryView_CategoryDetails> GetCategoryAsync()
        {
            return GetAsync<CategoryView_CategoryDetails>($"categoryview/@top");
        }
    }
}
