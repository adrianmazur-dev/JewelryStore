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
        Task<ProductImage?> GetByIdAsync(int id);
        Task DeleteImageAsync(int id);

        Task IncrementOrderAsync(int imageId);
        Task DecrementOrderAsync(int imageId);

        Task<bool> IsFirstInOrderAsync(int imageId);
        Task<bool> IsLastInOrderAsync(int imageId);

        Task UploadImageAsync(IFormFile file, int productId);
        Task<byte[]> GetFileMainByIdAsync(int id);
    }
}
