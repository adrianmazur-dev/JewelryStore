using AutoMapper;
using JewelryStore.Application.Interfaces;
using JewelryStore.Application.Services;
using JewelryStore.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace JewelryStore.Web.ViewComponents
{
    public class GalleryProductViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public GalleryProductViewComponent(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            return View(_mapper.Map<ProductViewModel>(product));
        }
    }
}
