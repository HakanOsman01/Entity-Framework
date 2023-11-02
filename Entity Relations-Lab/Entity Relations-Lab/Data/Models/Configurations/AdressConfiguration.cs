using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Relations_Lab.Data.Models.Configurations
{
    public class AdressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder
               .Property(e => e.AddressId)
               .HasColumnName("AddressID");

                builder.Property(e => e.AddressText)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                builder
                .Property(e => e.TownId)
                .HasColumnName("TownID");

                builder
                .HasOne(d => d.Town)
                .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.TownId)
                    .HasConstraintName("FK_Addresses_Towns");
           
        }
    }
}
