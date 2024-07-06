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
    internal class NeighbourhoodConfiguration : IEntityTypeConfiguration<Neighbourhood>
    {
        public void Configure ( EntityTypeBuilder<Neighbourhood> builder )
        {
            builder.HasKey(k => k.NeighbourhoodId);
            builder.Property(k => k.NeighbourhoodId).UseIdentityColumn();
            builder.Property(k => k.NeighbourhoodName).IsRequired(true).HasMaxLength(100);
            builder.Property(k => k.ZipCode).IsRequired(true).HasMaxLength(20);
            builder.Property(k=>k.DistrictId).IsRequired(true);
        }
    }
}
