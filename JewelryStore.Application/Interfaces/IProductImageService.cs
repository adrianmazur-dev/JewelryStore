using JewelryStore.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.Application.Interfaces
{
    public interface IProductImageService
    {
        Task UploadImageAsync(IFormFile file, int productId);
        Task<ProductImage> GetByIdAsync(int id);
        Task<byte[]> GetFileMainByIdAsync(int id);
        Task DeleteImageAsync(int id);

        Task<(bool isFirst, bool isLast)> GetImagePositionAsync(int imageId);

        Task IncrementOrderAsync(int imageId);
        Task DecrementOrderAsync(int imageId);
    }
}
