using JewelryStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.Infrastructure.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasKey(pi => pi.Id);

            builder.Property(pi => pi.FileName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Order)
               .IsRequired()
               .HasDefaultValue(0);

            builder.HasIndex(x => new { x.ProductId, x.Order });

            builder.ToTable(t => t.HasCheckConstraint("CK_ProductImage_Order", "[Order] >= 0"));

            builder.HasOne(pi => pi.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(pi => pi.FileName);
        }
    }
}
