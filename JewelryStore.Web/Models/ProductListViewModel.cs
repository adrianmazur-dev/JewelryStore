using JewelryStore.Application.DTOs;

namespace JewelryStore.Web.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; } = [];
        public IEnumerable<CategoryDto> Categories { get; set; } = [];
        public int? CurrentCategory { get; set; }
        public string? CurrentSearch { get; set; }
        public decimal? CurrentMinPrice { get; set; }
        public decimal? CurrentMaxPrice { get; set; }
        public string? CurrentSortOrder { get; set; }
    }
}
