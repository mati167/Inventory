using Inventory.Core.DAO;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Data.Configurations.LIBROS.COMIC
{
    internal class mangaConfiguration : IEntityTypeConfiguration<Manga>
    {
        public void Configure(EntityTypeBuilder<Manga> entity)
        {
            entity.HasKey(e => e.Idmanga);

            entity.ToTable("Manga");

            entity.Property(e => e.Idmanga).HasColumnName("IDManga");
            entity.Property(e => e.Idbook).HasColumnName("IDBook");
            entity.Property(e => e.Isbn).HasColumnName("ISBN");

            entity.HasOne(d => d.ArtistNavigation).WithMany(p => p.MangaArtistNavigations)
                .HasForeignKey(d => d.Artist)
                .HasConstraintName("FK_Manga_PersonArtist");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.MangaAuthorNavigations)
                .HasForeignKey(d => d.Author)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Manga_PersonAuthor");

            entity.HasOne(d => d.DemographyNavigation).WithMany(p => p.Mangas)
                .HasForeignKey(d => d.Demography)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Manga_Demography");

            entity.HasOne(d => d.IdbookNavigation).WithMany(p => p.Mangas)
                .HasForeignKey(d => d.Idbook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Manga_Book");

            entity.HasOne(d => d.SizeNavigation).WithMany(p => p.Mangas)
                .HasForeignKey(d => d.Size)
                .HasConstraintName("FK_Manga_Size");
        }
    }
}
