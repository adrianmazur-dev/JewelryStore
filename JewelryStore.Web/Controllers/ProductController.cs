using AutoMapper;
using JewelryStore.Application.DTOs;
using JewelryStore.Application.Interfaces;
using JewelryStore.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace JewelryStore.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(
            ILogger<ProductController> logger,
            IProductService productService,
            IMapper mapper) : base(mapper, logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
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

        public Task<IActionResult> Details(int productId) => ExecuteWithErrorHandlingAsync(async () =>
        {
            var product = await _productService.GetProductByIdAsync(productId);
            return Mapper.Map<ProductViewModel>(product);
        }, "Failed to load product details page");
    }
}
