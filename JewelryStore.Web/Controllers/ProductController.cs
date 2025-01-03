using AutoMapper;
using JewelryStore.Application.DTOs;
using JewelryStore.Application.Interfaces;
using JewelryStore.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace JewelryStore.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public ProductController(
            ILogger<ProductController> logger,
            IProductService productService,
            ICategoryService categoryService,
            IMapper mapper) : base(mapper, logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        public Task<IActionResult> Index(int? categoryId) => ExecuteWithErrorHandlingAsync(async () =>
        {
            IEnumerable<ProductDto> products;
            if (categoryId.HasValue)
            {
                products = await _productService.GetProductByCategoryIdAsync(categoryId.Value);
            }
            else
            {
                products = await _productService.GetAllProductsAsync();
            }
            return Mapper.Map<IEnumerable<ProductViewModel>>(products);
        }, "Failed to load products page");

        //search=&category=&minPrice=&maxPrice=&sortOrder=name_asc
        public Task<IActionResult> List(int? categoryId) => ExecuteWithErrorHandlingAsync(async () =>
        {
            IEnumerable<ProductDto> products;
            if (categoryId.HasValue)
            {
                products = await _productService.GetProductByCategoryIdAsync(categoryId.Value);
            }
            else
            {
                products = await _productService.GetAllProductsAsync();
            }

            return new ProductListViewModel
            {
                Products = Mapper.Map<IEnumerable<ProductViewModel>>(products),
                Categories = await _categoryService.GetAllAsync(),
                CurrentCategory = categoryId
            };
        }, "Failed to load products list page");

        public Task<IActionResult> Details(int productId) => ExecuteWithErrorHandlingAsync(async () =>
        {
            var product = await _productService.GetByIdAsync(productId);
            return Mapper.Map<ProductViewModel>(product);
        }, "Failed to load product details page");

        public Task<IActionResult> Edit(int productId) => ExecuteWithErrorHandlingAsync(async () =>
        {
            var product = await _productService.GetByIdAsync(productId);
            return product;
        }, "Failed to load product edit page");

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDto model)
        {
            if (!ModelState.IsValid)
                return View(model);    

            await _productService.UpdateAsync(model);

            return RedirectToAction("Edit", new { productId = model.Id});
        }
    }
}
