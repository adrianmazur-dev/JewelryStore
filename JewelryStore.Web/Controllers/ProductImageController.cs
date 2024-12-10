using AutoMapper;
using JewelryStore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JewelryStore.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductImageController : BaseController
    {
        private readonly IProductImageService _productImageService;

        public ProductImageController(
            ILogger<ProductImageController> logger,
            IProductImageService productImageService,
            IMapper mapper) : base(mapper, logger)
        {
            _productImageService = productImageService ?? throw new ArgumentNullException(nameof(productImageService));
        }

        [HttpPost("upload/{productId}")]
        public async Task<IActionResult> Upload(IFormFile file, int productId)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded");

                await _productImageService.UploadImageAsync(file, productId);
                return Ok("Image uploaded successfully.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error uploading file for product {ProductId}", productId);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            await _productImageService.DeleteImageAsync(id);
            return Ok("Image deleted successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var imageData = await _productImageService.GetFileMainByIdAsync(id);
            if (imageData == null)
                return NotFound();

            return File(imageData, "image/png");
        }

        [HttpPost("reorder/{id}/{direction}")]
        public async Task<IActionResult> Reorder(int id, string direction)
        {
            var image = await _productImageService.GetByIdAsync(id);
            if (image == null) return NotFound();

            if (direction == "up")
                await _productImageService.DecrementOrderAsync(id);
            else if (direction == "down")
                await _productImageService.IncrementOrderAsync(id);

            return Ok();
        }
    }
}
