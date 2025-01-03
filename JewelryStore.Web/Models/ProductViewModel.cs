using JewelryStore.Application.DTOs;
using JewelryStore.Domain.Entities;

namespace JewelryStore.Web.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int? CategoryId { get; set; }
        public CategoryDto? Category { get; set; }
        public ICollection<ProductImage> Images { get; set; } = [];

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
