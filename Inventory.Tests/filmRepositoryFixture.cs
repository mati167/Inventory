using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Repositories;
using Inventory.Core.DAO;
using System.Collections.Generic;

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
            var country = new Country { Idcountry = 1, CountryName = "Argentina" };
            var genre = new Genre { Idgenre = 1, Description = "Drama" };
            var director = new Person { Idpersona = 1, Name = "Juan", LastName = "Perez" };
            var actor = new Person { Idpersona = 2, Name = "Ana", LastName = "Lopez" };

            var film = new Film
            {
                Idfilm = 1,
                FilmName = "El Secreto de Sus Ojos",
                Year = 2009,
                Duration = TimeOnly.Parse("02:10"),
                Idcountries = new List<Country> { country },
                Idgenres = new List<Genre> { genre },
                idDirected = new List<Person> { director },
                idActed = new List<Person> { actor }
            };

            context.Countries.Add(country);
            context.Genres.Add(genre);
            context.People.AddRange(director, actor);
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
