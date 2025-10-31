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
            entity.HasKey(e => e.Idcontinent).HasName("PK_continente");

            entity.ToTable("Continent");

            entity.Property(e => e.Idcontinent).HasColumnName("IDContinent");
            entity.Property(e => e.ContinentName)
                .HasMaxLength(45)
                .IsUnicode(false);
        }
    }
}
