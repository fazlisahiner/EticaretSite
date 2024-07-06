using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaret.Core.ETicaretModel;



namespace ETicaret.Repository.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(k => k.CategoryId);
            builder.Property(k=>k.CategoryId).UseIdentityColumn();
            builder.Property(k => k.CategoryName).HasMaxLength(100);
            builder.Property(k => k.IsActive).IsRequired(false).HasDefaultValue(1);
        }

    }
}
