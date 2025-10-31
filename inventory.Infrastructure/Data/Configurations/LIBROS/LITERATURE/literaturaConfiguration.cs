using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Core.Entities.DAO;

namespace Inventory.Infrastructure.Data.Configurations.LIBROS.LITERATURE
{
    public class literaturaConfiguration : IEntityTypeConfiguration<Literature>
    {
        public void Configure(EntityTypeBuilder<Literature> entity)
        {
            entity.HasKey(e => e.Idliterature);

            entity.ToTable("Literature");

            entity.Property(e => e.Idliterature).HasColumnName("IDLiterature");
            entity.Property(e => e.Idbook).HasColumnName("IDBook");
            entity.Property(e => e.Isbn).HasColumnName("ISBN");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Literatures)
                .HasForeignKey(d => d.Author)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Literature_Person");

            entity.HasOne(d => d.IdbookNavigation).WithMany(p => p.Literatures)
                .HasForeignKey(d => d.Idbook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Literature_Book");
        }
    }
}
