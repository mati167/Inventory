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
    public class ContinentConfiguration : IEntityTypeConfiguration<Continent>
    {
        public void Configure(EntityTypeBuilder<Continent> entity)
        {
            entity.HasKey(e => e.idcontinent).HasName("PK_continente");

            entity.ToTable("continent");

            entity.Property(e => e.idcontinent).HasColumnName("idcontinent");
            entity.Property(e => e.continentname)
                .HasMaxLength(45)
                .IsUnicode(false);
        }
    }
}
