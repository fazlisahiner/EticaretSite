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
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure (EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(k => k.OrderId);
            builder.Property(k => k.OrderId).UseIdentityColumn();
            builder.Property(k => k.UserId).IsRequired(true);
            builder.Property(k => k.OrderDate).IsRequired(true).HasDefaultValue(DateTime.Now);
            builder.Property(k => k.TotalPrice).IsRequired(true).HasColumnType("decimal(10,2)");
            builder.Property(k => k.OrderStatus).IsRequired(true);
            builder.Property(k => k.EmplooyeeId).IsRequired(true);
        }
    }
}
