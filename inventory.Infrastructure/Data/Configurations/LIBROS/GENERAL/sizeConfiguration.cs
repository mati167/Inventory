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
    internal class sizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> entity)
        {
            entity.HasKey(e => e.Idsize);

            entity.ToTable("Size");

            entity.Property(e => e.Idsize).HasColumnName("IDSize");
            entity.Property(e => e.SizeName)
                .HasMaxLength(15)
                .IsUnicode(false);
        }
    }
}
