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
    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            //throw new NotImplementedException();

            builder.HasKey(x => x.AddressId);
            builder.Property(k => k.AddressId).UseIdentityColumn();
            builder.Property(k=> k.DistrictId).IsRequired(false);
            builder.Property(k => k.NeighbourhoodId).IsRequired(false);
            builder.Property(k => k.AddressText).IsRequired(true).HasMaxLength(250);
        }
    }
}
