using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaret.Core.ETicaretModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETicaret.Repository.Configurations
{
    internal class CityConfiguration :IEntityTypeConfiguration<City>
    {
        public void Configure (EntityTypeBuilder<City> builder)
        {
            builder.HasKey(k=>k.CityId);
            builder.Property(k => k.CityId).UseIdentityColumn();
            builder.Property(k => k.CountryId).IsRequired(true);
            builder.Property(k => k.CityName).IsRequired(true).HasMaxLength(100);
            builder.Property(k => k.PlateNo).IsRequired(false).HasMaxLength(2);
            builder.Property(k => k.PhoneCode).IsRequired(true).HasMaxLength(7);

        }
    }
}
