using Inventory.Core.Entities.DAO;
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
            entity.HasKey(e => e.idfilm).HasName("PK_Pelicula");

            entity.ToTable("film");

            entity.Property(e => e.idfilm).HasColumnName("idfilm");
            entity.Property(e => e.filmname)
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.Property(e => e.imdbid)
    .HasMaxLength(100)
    .IsUnicode(false);

            entity.HasMany(d => d.idcountries).WithMany(p => p.idfilms)
                .UsingEntity<Dictionary<string, object>>(
                    "FilmCountry",
                    r => r.HasOne<Country>().WithMany()
                        .HasForeignKey("idcountry")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_se_filmo_en_pais"),
                    l => l.HasOne<Film>().WithMany()
                        .HasForeignKey("idfilm")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_se_filmo_en_pelicula"),
                    j =>
                    {
                        j.HasKey("idfilm", "idcountry").HasName("PK_se_filmo_en");
                        j.ToTable("film_country");
                        j.IndexerProperty<int>("idfilm").HasColumnName("idfilm");
                        j.IndexerProperty<int>("idcountry").HasColumnName("idcountry");
                    });

            entity.HasMany(d => d.idgenres).WithMany(p => p.idfilms)
                .UsingEntity<Dictionary<string, object>>(
                    "filmgenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("idgenre")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_pertenece_genero"),
                    l => l.HasOne<Film>().WithMany()
                        .HasForeignKey("idfilm")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_pertenece_pelicula"),
                    j =>
                    {
                        j.HasKey("idfilm", "idgenre").HasName("PK_pertenece");
                        j.ToTable("film_genre");
                        j.IndexerProperty<int>("idfilm").HasColumnName("idfilm");
                        j.IndexerProperty<int>("idgenre").HasColumnName("idgenre");
                    });

            entity.HasMany(d => d.iddirected).WithMany(p => p.Idfilms)
                .UsingEntity<Dictionary<string, object>>(
                    "directed",
                    r => r.HasOne<Person>().WithMany()
                        .HasForeignKey("idperson")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_es_dirigida_persona"),
                    l => l.HasOne<Film>().WithMany()
                        .HasForeignKey("idfilm")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_es_dirigida_pelicula"),
                    j =>
                    {
                        j.HasKey("idfilm", "idperson").HasName("PK_es_dirigida");
                        j.ToTable("directed");
                        j.IndexerProperty<int>("idfilm").HasColumnName("idfilm");
                        j.IndexerProperty<int>("idperson").HasColumnName("idperson");
                    });

            entity.HasMany(d => d.idacted).WithMany(p => p.IdfilmsNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "played",
                    r => r.HasOne<Person>().WithMany()
                        .HasForeignKey("idperson")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_es_actuada_persona"),
                    l => l.HasOne<Film>().WithMany()
                        .HasForeignKey("idfilm")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_es_actuada_pelicula"),
                    j =>
                    {
                        j.HasKey("idfilm", "idperson").HasName("PK_es_actuada");
                        j.ToTable("played");
                        j.IndexerProperty<int>("idfilm").HasColumnName("idfilm");
                        j.IndexerProperty<int>("idperson").HasColumnName("idperson");
                    });
        }
    }
}
