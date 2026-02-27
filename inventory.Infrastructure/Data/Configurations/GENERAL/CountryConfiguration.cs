using Inventory.Core.Entities.DAO;
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
            entity.HasKey(e => e.idcountry).HasName("PK_pais");

            entity.ToTable("country");

            entity.Property(e => e.idcountry).HasColumnName("idcountry");
            entity.Property(e => e.countryname)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.idcontinent).HasColumnName("idcontinent");

            entity.HasOne(d => d.idcontinentNavigation).WithMany(p => p.countries)
                .HasForeignKey(d => d.idcontinent)
                .HasConstraintName("PK_pais_continente");
        }
    }
}
