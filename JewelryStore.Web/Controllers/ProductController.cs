using AutoMapper;
using JewelryStore.Application.Interfaces;
using JewelryStore.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JewelryStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            var viewModel = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return View(viewModel);
        }
    }
}
