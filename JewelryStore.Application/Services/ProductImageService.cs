using JewelryStore.Application.Interfaces;
using JewelryStore.Domain.Entities;
using JewelryStore.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.Application.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly string _uploadPath;

        public ProductImageService(IProductImageRepository productImageRepository, IConfiguration configuration)
        {
            _productImageRepository = productImageRepository;
            _uploadPath = configuration["UploadPath"] ?? string.Empty;
        }

        public async Task<ProductImage?> GetByIdAsync(int id)
        {
            var image = await _productImageRepository.GetByIdAsync(id);
            return image;
        }

        public async Task DeleteImageAsync(int id)
        {
            var image = await _productImageRepository.GetByIdAsync(id);
            if (image == null) 
                return;

            var filePath = Path.Combine(_uploadPath, image.FileName);
            if (File.Exists(filePath))
                File.Delete(filePath);

            while (await _productImageRepository.GetNextOrderImageAsync(image.ProductId, image.Order) is ProductImage lastImage)
            {
                await DecrementOrderAsync(lastImage.Id);
            }

            await _productImageRepository.DeleteAsync(id);
        }

        public async Task UploadImageAsync(IFormFile file, int productId)
        {
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(_uploadPath, fileName);

            if (!Directory.Exists(_uploadPath))
                throw new Exception("Lokalizacja nie istnieje!");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            int order = 0;

            var lastImage = await _productImageRepository.GetLastOrderImageAsync(productId);
            if (lastImage != null)
                order = lastImage.Order + 1;

            var image = new ProductImage
            {
                ProductId = productId,
                FileName = fileName,
                UploadDate = DateTime.UtcNow,
                Order = order
            };

            await _productImageRepository.AddAsync(image);
        }

        public async Task IncrementOrderAsync(int imageId)
        {
            var image = await _productImageRepository.GetByIdAsync(imageId);
            if (image == null) 
                return;

            var nextImage = await _productImageRepository.GetNextOrderImageAsync(image.ProductId, image.Order);
            if (nextImage != null)
            {
                (image.Order, nextImage.Order) = (nextImage.Order, image.Order);
                await _productImageRepository.UpdateAsync([image, nextImage]);
            }
        }

        public async Task DecrementOrderAsync(int imageId)
        {
            var image = await _productImageRepository.GetByIdAsync(imageId);
            if (image == null) 
                return;

            var prevImage = await _productImageRepository.GetPreviousOrderImageAsync(image.ProductId, image.Order);
            if (prevImage != null)
            {
                (image.Order, prevImage.Order) = (prevImage.Order, image.Order);
                await _productImageRepository.UpdateAsync([image, prevImage]);
            }
        }

        public async Task<byte[]> GetFileMainByIdAsync(int id)
        {
            var image = await _productImageRepository.GetByIdAsync(id);
            if (image == null) 
                return [];

            return await File.ReadAllBytesAsync(await GetImagePathAsync(id));
        }

        public async Task<string> GetImagePathAsync(int id)
        {
            var image = await _productImageRepository.GetByIdAsync(id);
            if (image == null) 
                return string.Empty;

            return Path.Combine(_uploadPath, image.FileName);
        }

        public async Task<bool> IsFirstInOrderAsync(int imageId)
        {
            var firstImage = await _productImageRepository.GetFirstOrderImageAsync(imageId);
            if (firstImage == null) 
                return false;

            return firstImage.Id == imageId;
        }

        public async Task<bool> IsLastInOrderAsync(int imageId)
        {
            var lastImage = await _productImageRepository.GetLastOrderImageAsync(imageId);
            if (lastImage == null)
                return false;

            return lastImage.Id == imageId;
        }
    }
}
