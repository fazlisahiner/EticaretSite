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
    internal class TownConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasKey(k => k.TownId);
            builder.Property(k => k.TownId).UseIdentityColumn();
            builder.Property(k => k.CityId).IsRequired(true);
            builder.Property(k => k.TownName).IsRequired(true).HasMaxLength(100);
        }
    }
}
