using JewelryStore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JewelryStore.Web.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryMenuViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetMainCategoriesWithSubsAsync();
            return View(categories);
        }
    }
}
