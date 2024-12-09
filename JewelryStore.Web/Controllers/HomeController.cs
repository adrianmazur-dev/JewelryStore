using System.Diagnostics;
using AutoMapper;
using JewelryStore.Application.Interfaces;
using JewelryStore.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace JewelryStore.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductService _productService;

        public HomeController(
            ILogger<HomeController> logger,
            IProductService productService,
            IMapper mapper) : base(mapper, logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public Task<IActionResult> Index() => ExecuteWithErrorHandlingAsync(async () =>
        {
            var products = await _productService.GetAllProductsAsync();
            return new HomeViewModel
            {
                FeaturedProducts = Mapper.Map<List<ProductViewModel>>(products),
                NewsletterEnabled = true
            };
        }, "Failed to load home page");

        public IActionResult Privacy() => View();


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}
