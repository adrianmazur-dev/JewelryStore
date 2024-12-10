using AutoMapper;
using JewelryStore.Application.DTOs;
using JewelryStore.Application.Interfaces;
using JewelryStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetProductByCategoryIdAsync(int categoryId)
        {
            var products = await _productRepository.GetByCategoryIdAsync(categoryId);

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public Task UpdateAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Domain.Entities.Product>(productDto);
            return _productRepository.UpdateAsync(product);
        }
    }
}
