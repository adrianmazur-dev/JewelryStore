using JewelryStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.Domain.Interfaces
{
    public interface IProductImageRepository
    {
        Task<ProductImage?> GetByIdAsync(int id);
        Task<List<ProductImage>> GetByProductIdAsync(int productId);

        Task UpdateAsync(params ProductImage[] images);
        Task AddAsync(ProductImage image);
        Task DeleteAsync(int id);

        Task<ProductImage?> GetFirstOrderImageAsync(int productId);
        Task<ProductImage?> GetNextOrderImageAsync(int productId, int currentOrder);
        Task<ProductImage?> GetPreviousOrderImageAsync(int productId, int currentOrder);
        Task<ProductImage?> GetLastOrderImageAsync(int productId);
    }
}
