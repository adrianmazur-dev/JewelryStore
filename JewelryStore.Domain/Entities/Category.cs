using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }

        public ICollection<Product> Products { get; set; } = [];

        public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; } = null;
        public ICollection<Category> SubCategories { get; set; } = [];
    }
}
