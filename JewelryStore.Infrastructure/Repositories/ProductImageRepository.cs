using JewelryStore.Domain.Entities;
using JewelryStore.Domain.Interfaces;
using JewelryStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.Infrastructure.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly JewelryStoreDbContext _context;

        public ProductImageRepository(JewelryStoreDbContext context)
        {
            _context = context;
        }

        public async Task<ProductImage?> GetByIdAsync(int id)
        {
            var image = await _context.ProductImages
                .FindAsync(id);
            return image;
        }

        public async Task<List<ProductImage>> GetByProductIdAsync(int productId)
        {
            var images = await _context.ProductImages
                .Where(i => i.ProductId == productId)
                .Include(image => image.Product)
                .ToListAsync();
            return images;
        }

        public async Task<ProductImage?> GetNextOrderImageAsync(int productId, int currentOrder)
        {
            return await _context.ProductImages
                .Where(x => x.ProductId == productId && x.Order > currentOrder)
                .OrderBy(x => x.Order)
                .FirstOrDefaultAsync();
        }

        public async Task<ProductImage?> GetPreviousOrderImageAsync(int productId, int currentOrder)
        {
            return await _context.ProductImages
                .Where(x => x.ProductId == productId && x.Order < currentOrder)
                .OrderByDescending(x => x.Order)
                .FirstOrDefaultAsync();
        }

        public async Task<ProductImage?> GetLastOrderImageAsync(int productId)
        {
            return await _context.ProductImages
                .Where(x => x.ProductId == productId)
                .OrderBy(x => x.Order)
                .LastOrDefaultAsync();
        }

        public async Task<ProductImage?> GetFirstOrderImageAsync(int productId)
        {
            return await _context.ProductImages
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.Order)
                .LastOrDefaultAsync();
        }

        public async Task UpdateAsync(params ProductImage[] images)
        {
            _context.ProductImages.UpdateRange(images);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(ProductImage image)
        {
            await _context.ProductImages.AddAsync(image);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var image = await GetByIdAsync(id);
            if (image != null)
            {
                _context.ProductImages.Remove(image);
                await _context.SaveChangesAsync();
            }
        }
    }
}
