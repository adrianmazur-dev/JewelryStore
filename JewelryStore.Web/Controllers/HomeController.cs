using System.Diagnostics;
using JewelryStore.Web.Models;
using JewelryStore.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JewelryStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                FeaturedProducts = new List<ProductViewModel>
                {
                    new() { Name = "Gold Necklace" },
                    new() { Name = "Diamond Ring"},
                    new() { Name = "Silver Bracelet"}
                },
                NewsletterEnabled = true
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
