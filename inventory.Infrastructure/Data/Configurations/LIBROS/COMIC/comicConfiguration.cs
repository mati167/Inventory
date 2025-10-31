using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Core.Entities.DAO;

namespace Inventory.Infrastructure.Data.Configurations.LIBROS.COMIC
{
    public class comicConfiguration : IEntityTypeConfiguration<Comic>
    {
        public void Configure(EntityTypeBuilder<Comic> entity)
        {
            entity.HasKey(e => e.Idcomic);

            entity.ToTable("Comic");

            entity.Property(e => e.Idcomic).HasColumnName("IDComic");
            entity.Property(e => e.Idbook).HasColumnName("IDBook");
            entity.Property(e => e.Isbn).HasColumnName("ISBN");

            entity.HasOne(d => d.ArtistNavigation).WithMany(p => p.ComicArtistNavigations)
                .HasForeignKey(d => d.Artist)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comic_PersonArtist");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.ComicAuthorNavigations)
                .HasForeignKey(d => d.Author)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comic_PersonAuthor");

            entity.HasOne(d => d.IdbookNavigation).WithMany(p => p.Comics)
                .HasForeignKey(d => d.Idbook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comic_Book");
        }
    }
}
