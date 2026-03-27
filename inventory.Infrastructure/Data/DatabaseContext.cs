using Inventory.Core.Entities.DAO;
using Inventory.Infrastructure.Data.Configurations.FILMS;
using Inventory.Infrastructure.Data.Configurations.GENERAL;
using Inventory.Infrastructure.Data.Configurations.LIBROS.COMIC;
using Inventory.Infrastructure.Data.Configurations.LIBROS.GENERAL;
using Inventory.Infrastructure.Data.Configurations.LIBROS.LITERATURE;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Data
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
    : base(options)
        { }
        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Comic> Comics { get; set; }

        public virtual DbSet<Continent> Continents { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Demography> Demographies { get; set; }

        public virtual DbSet<Film> Films { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }

        public virtual DbSet<Literature> Literatures { get; set; }

        public virtual DbSet<Manga> Mangas { get; set; }

        public virtual DbSet<Person> Person { get; set; }

        public virtual DbSet<Publisher> Publishers { get; set; }

        public virtual DbSet<Size> Sizes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdministratorConfiguration());
            modelBuilder.ApplyConfiguration(new bookConfiguration());
            modelBuilder.ApplyConfiguration(new comicConfiguration());
            modelBuilder.ApplyConfiguration(new ContinentConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new DemograpgyConfiguration());
            modelBuilder.ApplyConfiguration(new filmConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new literaturaConfiguration());
            modelBuilder.ApplyConfiguration(new mangaConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new publisherConfiguration());
            modelBuilder.ApplyConfiguration(new sizeConfiguration());


            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
