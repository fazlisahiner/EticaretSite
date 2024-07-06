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
    internal class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure (EntityTypeBuilder<District> builder)
        {
            builder.HasKey(k => k.DistrictId);
            builder.Property(k => k.DistrictId).UseIdentityColumn();
            builder.Property(k => k.DistrictName).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.TownId);
        }
    }
}
