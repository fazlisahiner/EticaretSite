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
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(k=>k.UserId);
            builder.Property(k => k.UserId).UseIdentityColumn();
            builder.Property(k=>k.FirstName).IsRequired(true).HasMaxLength(100);
            builder.Property(k=>k.LastName).IsRequired(true).HasMaxLength(100);
            builder.Property(k => k.Email).IsRequired(true).HasMaxLength(120);
            builder.Property(k => k.Password).IsRequired(true).HasMaxLength(100);
            builder.Property(k => k.TelNumber1).IsRequired(true).HasMaxLength(13);
            builder.Property(k => k.TelNumber2).IsRequired(false).HasMaxLength(13);
            builder.Property(k => k.Gender).IsRequired(false).HasMaxLength(5);
            builder.Property(k => k.CreateDate).IsRequired(true).HasDefaultValue(DateTime.Now);
            builder.Property(k => k.BirthDate).IsRequired(false);
            builder.Property(k => k.IsActive).IsRequired(false).HasDefaultValue(1); 


        }
    }
}
