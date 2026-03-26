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
            entity.HasKey(e => e.idpersona).HasName("PK_persona");

            entity.ToTable("person");

            entity.Property(e => e.idpersona)
                .HasColumnName("idpersona");
            entity.Property(e => e.lastname)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.name)
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.HasMany(d => d.Idcountries).WithMany(p => p.idpeople)
                .UsingEntity<Dictionary<string, object>>(
                    "nacionality",
                    r => r.HasOne<Country>().WithMany()
                        .HasForeignKey("idcountry")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_nacionalidad_pais"),
                    l => l.HasOne<Person>().WithMany()
                        .HasForeignKey("idperson")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_nacionalidad_persona"),
                    j =>
                    {
                        j.HasKey("idperson", "idcountry").HasName("PK_nacionalidad");
                        j.ToTable("nacionality");
                        j.IndexerProperty<int>("idperson").HasColumnName("idperson");
                        j.IndexerProperty<int>("idcountry").HasColumnName("idcountry");
                    });
        }
    }
}
