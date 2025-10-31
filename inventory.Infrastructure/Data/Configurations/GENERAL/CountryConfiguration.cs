using Inventory.Core.DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Data.Configurations.GENERAL
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> entity)
        {
            entity.HasKey(e => e.Idcountry).HasName("PK_pais");

            entity.ToTable("Country");

            entity.Property(e => e.Idcountry).HasColumnName("IDCountry");
            entity.Property(e => e.CountryName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Idcontinent).HasColumnName("IDContinent");

            entity.HasOne(d => d.IdcontinentNavigation).WithMany(p => p.Countries)
                .HasForeignKey(d => d.Idcontinent)
                .HasConstraintName("PK_pais_continente");
        }
    }
}
