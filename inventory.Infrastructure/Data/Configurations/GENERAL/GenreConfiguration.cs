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
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> entity)
        {
            entity.HasKey(e => e.idgenre).HasName("PK_genero");

            entity.ToTable("genre");

            entity.Property(e => e.idgenre).HasColumnName("idgenre");
            entity.Property(e => e.description)
                .HasMaxLength(25)
                .IsUnicode(false);
        }
    }
}
