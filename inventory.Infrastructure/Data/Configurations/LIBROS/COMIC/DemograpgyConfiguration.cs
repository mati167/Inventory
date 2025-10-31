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
    internal class DemograpgyConfiguration : IEntityTypeConfiguration<Demography>
    {
        public void Configure(EntityTypeBuilder<Demography> entity)
        {
            entity.HasKey(e => e.Iddemography);

            entity.ToTable("Demography");

            entity.Property(e => e.Iddemography).HasColumnName("IDDemography");
            entity.Property(e => e.DemographyDescription).IsUnicode(false);
        }
    }
}

