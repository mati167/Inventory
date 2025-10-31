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
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> entity)
        {
            entity.HasKey(e => e.Idpersona).HasName("PK_persona");

            entity.ToTable("Person");

            entity.Property(e => e.Idpersona)
                .ValueGeneratedNever()
                .HasColumnName("IDPersona");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.HasMany(d => d.Idcountries).WithMany(p => p.Idpeople)
                .UsingEntity<Dictionary<string, object>>(
                    "Nacionality",
                    r => r.HasOne<Country>().WithMany()
                        .HasForeignKey("Idcountry")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_nacionalidad_pais"),
                    l => l.HasOne<Person>().WithMany()
                        .HasForeignKey("Idperson")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_nacionalidad_persona"),
                    j =>
                    {
                        j.HasKey("Idperson", "Idcountry").HasName("PK_nacionalidad");
                        j.ToTable("Nacionality");
                        j.IndexerProperty<int>("Idperson").HasColumnName("IDPerson");
                        j.IndexerProperty<int>("Idcountry").HasColumnName("IDCountry");
                    });
        }
    }
}
