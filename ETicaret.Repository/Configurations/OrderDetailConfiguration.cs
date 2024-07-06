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
    internal class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure ( EntityTypeBuilder<OrderDetail> builder )
        {
            builder.HasKey(k => k.DetailId);
            builder.Property(k => k.DetailId).UseIdentityColumn();
            builder.Property(k => k.OrderId).IsRequired(true);
            builder.Property(k => k.ProductId).IsRequired(true);
            builder.Property(k => k.Quantity).IsRequired(true);
            builder.Property(k => k.ProductPrice).IsRequired(true).HasColumnType("decimal(10,2)");
            builder.Property(k => k.Discount).IsRequired(false).HasColumnType("decimal(10,2)");
            builder.Property(k => k.LineTotal).IsRequired(true).HasColumnType("decimal(10,2)");
        }
    }
}
