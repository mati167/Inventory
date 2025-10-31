using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Core.Entities.DAO;

namespace Inventory.Infrastructure.Data.Configurations.LIBROS.GENERAL
{
    internal class publisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> entity)
        {
            entity.HasKey(e => e.Idpublisher);

            entity.ToTable("Publisher");

            entity.Property(e => e.Idpublisher).HasColumnName("IDPublisher");
            entity.Property(e => e.PublisherName)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}