using ETicaret.Core.ETicaretModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Repository.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure (EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(k => k.ProductId);
            builder.Property(k => k.ProductId).UseIdentityColumn();
            builder.Property(k => k.ProductCode).IsRequired(false).HasMaxLength(20);
            builder.Property(k => k.ProductName).IsRequired(true).HasMaxLength(100);
            builder.Property(k => k.Price).IsRequired(true).HasColumnType("decimal(10,2)");
            builder.Property(k => k.StockQuantity).IsRequired(true);
            builder.Property(k => k.CreateDate).IsRequired(true).HasDefaultValue(DateTime.Now);
            builder.Property(k => k.UpdateDate).IsRequired(true).HasDefaultValue(DateTime.Now);
            builder.Property(k => k.Description).IsRequired(false).HasMaxLength(200);
            builder.Property(k => k.CategoryId).IsRequired(true);
            builder.Property(k => k.Brand).IsRequired(false).HasMaxLength(50);
            builder.Property(k => k.ImageUrl).IsRequired(false).HasMaxLength(200);
        }
    }
}
