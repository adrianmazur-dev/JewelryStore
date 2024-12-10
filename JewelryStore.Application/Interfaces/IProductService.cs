using JewelryStore.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<IEnumerable<ProductDto>> GetProductByCategoryIdAsync(int categoryId);
        Task<ProductDto> GetByIdAsync(int id);

        Task UpdateAsync(ProductDto productDto);
    }
}
