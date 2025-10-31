using Inventory.Core.DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Data.Configurations.FILMS
{
    public class filmConfiguration : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> entity)
        {
            entity.HasKey(e => e.Idfilm).HasName("PK_Pelicula");

            entity.ToTable("Film");

            entity.Property(e => e.Idfilm).HasColumnName("IDFilm");
            entity.Property(e => e.FilmName)
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.HasMany(d => d.Idcountries).WithMany(p => p.Idfilms)
                .UsingEntity<Dictionary<string, object>>(
                    "FilmCountry",
                    r => r.HasOne<Country>().WithMany()
                        .HasForeignKey("Idcountry")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_se_filmo_en_pais"),
                    l => l.HasOne<Film>().WithMany()
                        .HasForeignKey("Idfilm")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_se_filmo_en_pelicula"),
                    j =>
                    {
                        j.HasKey("Idfilm", "Idcountry").HasName("PK_se_filmo_en");
                        j.ToTable("Film_Country");
                        j.IndexerProperty<int>("Idfilm").HasColumnName("IDFilm");
                        j.IndexerProperty<int>("Idcountry").HasColumnName("IDCountry");
                    });

            entity.HasMany(d => d.Idgenres).WithMany(p => p.Idfilms)
                .UsingEntity<Dictionary<string, object>>(
                    "FilmGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("Idgenre")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_pertenece_genero"),
                    l => l.HasOne<Film>().WithMany()
                        .HasForeignKey("Idfilm")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_pertenece_pelicula"),
                    j =>
                    {
                        j.HasKey("Idfilm", "Idgenre").HasName("PK_pertenece");
                        j.ToTable("Film_Genre");
                        j.IndexerProperty<int>("Idfilm").HasColumnName("IDFilm");
                        j.IndexerProperty<int>("Idgenre").HasColumnName("IDGenre");
                    });

            entity.HasMany(d => d.Idpeople).WithMany(p => p.Idfilms)
                .UsingEntity<Dictionary<string, object>>(
                    "Directed",
                    r => r.HasOne<Person>().WithMany()
                        .HasForeignKey("Idperson")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_es_dirigida_persona"),
                    l => l.HasOne<Film>().WithMany()
                        .HasForeignKey("Idfilm")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_es_dirigida_pelicula"),
                    j =>
                    {
                        j.HasKey("Idfilm", "Idperson").HasName("PK_es_dirigida");
                        j.ToTable("Directed");
                        j.IndexerProperty<int>("Idfilm").HasColumnName("IDFilm");
                        j.IndexerProperty<int>("Idperson").HasColumnName("IDPerson");
                    });

            entity.HasMany(d => d.IdpeopleNavigation).WithMany(p => p.IdfilmsNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "Played",
                    r => r.HasOne<Person>().WithMany()
                        .HasForeignKey("Idperson")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_es_actuada_persona"),
                    l => l.HasOne<Film>().WithMany()
                        .HasForeignKey("Idfilm")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_es_actuada_pelicula"),
                    j =>
                    {
                        j.HasKey("Idfilm", "Idperson").HasName("PK_es_actuada");
                        j.ToTable("Played");
                        j.IndexerProperty<int>("Idfilm").HasColumnName("IDFilm");
                        j.IndexerProperty<int>("Idperson").HasColumnName("IDPerson");
                    });
        }
    }
}
