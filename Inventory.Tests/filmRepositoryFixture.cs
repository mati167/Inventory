using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Repositories;
using System.Collections.Generic;
using Inventory.Core.Entities.DAO;

namespace Inventory.Tests
{
    public class filmRepositoryFixture : IDisposable
    {
        public DatabaseContext Context { get; private set; }
        public filmRepository Repository { get; private set; }
        private readonly SqliteConnection _connection;

        public filmRepositoryFixture()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlite(_connection)
                .Options;

            Context = new DatabaseContext(options);
            Context.Database.EnsureCreated();

            SeedData(Context);

            Repository = new filmRepository(Context, NullLogger<filmRepository>.Instance);
        }

        private static void SeedData(DatabaseContext context)
        {
            var country = new Country { idcountry = 1, countryname = "Argentina" };
            var genre = new Genre { idgenre = 1, description = "Drama" };
            var director = new Person { idpersona = 1, name = "Juan", lastname = "Perez" };
            var actor = new Person { idpersona = 2, name = "Ana", lastname = "Lopez" };

            var film = new Film
            {
                idfilm = 1,
                filmname = "El Secreto de Sus Ojos",
                year = 2009,
                duration = TimeOnly.Parse("02:10"),
                idcountries = new List<Country> { country },
                idgenres = new List<Genre> { genre },
                iddirected = new List<Person> { director },
                idacted = new List<Person> { actor }
            };

            context.Countries.Add(country);
            context.Genres.Add(genre);
            context.Person.AddRange(director, actor);
            context.Films.Add(film);
            context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
            _connection.Dispose();
        }
    }
}
