using Inventory.Core.Entities.DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Infrastructure.Data.Configurations.GENERAL
{
    internal class AdministratorConfiguration :IEntityTypeConfiguration<Administrator>
    {
        public void Configure(EntityTypeBuilder<Administrator> entity)
        {
            // Nombre de la tabla en Postgres
            entity.ToTable("administrator");

            // Llave Primaria
            entity.HasKey(e => e.Id).HasName("administrator_pkey");

            // Configuración de Columnas
            entity.Property(e => e.Id)
                .HasColumnName("id_administrator")
                .ValueGeneratedOnAdd(); // Esto mapea al SERIAL de Postgres

            entity.Property(e => e.Username)
                .HasColumnName("username")
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode(false);

            entity.Property(e => e.PasswordHash)
                .HasColumnName("password") // Se mapea a la columna 'password' de la DB
                .IsRequired()
                .IsUnicode(false);

            // Índice Único para el Username
            entity.HasIndex(e => e.Username)
                .IsUnique()
                .HasDatabaseName("administrator_username_key");
        }
    }
}
