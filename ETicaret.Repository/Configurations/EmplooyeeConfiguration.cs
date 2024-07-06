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
    internal class EmplooyeeConfiguration : IEntityTypeConfiguration<Emplooyee>
    {
        public void Configure (EntityTypeBuilder<Emplooyee> builder)
        {
            builder.HasKey(k => k.EmplooyeeId);
            builder.Property(k => k.EmplooyeeId).UseIdentityColumn();
            builder.Property(k => k.FirstName).IsRequired(true).HasMaxLength(100);
            builder.Property(k => k.LastName).IsRequired(true).HasMaxLength(100);
            builder.Property(k => k.Email).IsRequired(true).HasMaxLength(120);
            builder.Property(k => k.BirthDate).IsRequired(true);
            builder.Property(k => k.TelNumber1).IsRequired(true).HasMaxLength(13);
            builder.Property(k => k.CreateDate).IsRequired(true);
            builder.Property(k => k.HireDate).IsRequired(true);
            builder.Property(k => k.Gender).IsRequired(true).HasMaxLength(5);
            builder.Property(k => k.Position).IsRequired(true).HasMaxLength(100);
            builder.Property(k => k.CountryId);
            builder.Property(k => k.CityId);
            builder.Property(k => k.TownId);
            builder.Property(k => k.DistrictId);
            builder.Property(k => k.NeighbourhoodId);
            builder.Property(k => k.AddressText).IsRequired(true).HasMaxLength(150);
        }
    }
}
