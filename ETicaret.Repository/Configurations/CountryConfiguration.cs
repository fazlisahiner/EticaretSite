using Microsoft.EntityFrameworkCore;
using ETicaret.Core.ETicaretModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Repository.Configurations
{
    internal class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure (EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(k => k.CountryId);
            builder.Property(k => k.CountryId).UseIdentityColumn();
            builder.Property(k => k.BinaryCode).HasMaxLength(2);
            builder.Property(k => k.TripleCode).HasMaxLength(3);
            builder.Property(k => k.CountryName).HasMaxLength(100);
            builder.Property(k => k.PhoneCode).HasMaxLength(6);
        }
    }
}
