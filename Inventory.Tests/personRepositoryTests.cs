using Inventory.Core.Entities.DAO;
using Inventory.Core.Entities.DTOs.General;
using Inventory.Core.Entities.DTOs.Person;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Inventory.Tests.Repositories
{
    public class personRepositoryTests
    {
        private readonly DbContextOptions<DatabaseContext> _options;
        private readonly Mock<ILogger<personRepository>> _mockLogger;

        public personRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _mockLogger = new Mock<ILogger<personRepository>>();
        }

        private DatabaseContext CreateContext()
        {
            return new DatabaseContext(_options);
        }

        [Fact]
        public void AddPerson_ShouldAddAndReturnPerson()
        {
            // Arrange
            using var context = CreateContext();

            var country = new Country { Idcountry = 1, CountryName = "Argentina" };
            context.Countries.Add(country);
            context.SaveChanges();

            var repo = new personRepository(context, _mockLogger.Object);

            var dto = new CreatePersonDTO
            {
                Name = "Juan",
                LastName = "Pérez",
                Idcountries = new List<int> { 1 }
            };

            // Act
            var result = repo.addPerson(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Juan", result.Name);
            Assert.Single(result.Countries);
            Assert.Equal("Argentina", result.Countries.First().description);
        }

        [Fact]
        public void GetPersonById_ShouldReturnPerson_WhenExists()
        {
            using var context = CreateContext();

            var country = new Country { Idcountry = 1, CountryName = "Chile" };
            var person = new Person
            {
                Idpersona = 10,
                Name = "Pedro",
                LastName = "Gómez",
                Idcountries = new List<Country> { country }
            };
            context.Countries.Add(country);
            context.Person.Add(person);
            context.SaveChanges();

            var repo = new personRepository(context, _mockLogger.Object);

            // Act
            var result = repo.getPersonById(10);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Pedro", result.Name);
            Assert.Single(result.Countries);
        }

        [Fact]
        public void GetPersonById_ShouldThrow_WhenNotFound()
        {
            using var context = CreateContext();
            var repo = new personRepository(context, _mockLogger.Object);

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => repo.getPersonById(999));
            Assert.Contains("No se encontró la persona", ex.Message);
        }

        [Fact]
        public void GetPersonList_ShouldReturnOrderedList()
        {
            using var context = CreateContext();

            var country = new Country { Idcountry = 1, CountryName = "Uruguay" };
            var person1 = new Person { Idpersona = 1, Name = "Ana", LastName = "Zapata", Idcountries = new List<Country> { country } };
            var person2 = new Person { Idpersona = 2, Name = "Luis", LastName = "Alvarez", Idcountries = new List<Country> { country } };

            context.Countries.Add(country);
            context.Person.AddRange(person1, person2);
            context.SaveChanges();

            var repo = new personRepository(context, _mockLogger.Object);

            // Act
            var result = repo.GetPersonList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Alvarez", result.First().LastName); // ordenado por apellido
        }

        [Fact]
        public void UpdatePerson_ShouldModifyData()
        {
            using var context = CreateContext();

            var country1 = new Country { Idcountry = 1, CountryName = "Argentina" };
            var country2 = new Country { Idcountry = 2, CountryName = "Brasil" };

            var person = new Person
            {
                Idpersona = 1,
                Name = "Sofia",
                LastName = "Martinez",
                Idcountries = new List<Country> { country1 }
            };

            context.Countries.AddRange(country1, country2);
            context.Person.Add(person);
            context.SaveChanges();

            var repo = new personRepository(context, _mockLogger.Object);

            var dto = new updatePersonDTO
            {
                Idpersona = 1,
                Name = "Sofia Actualizada",
                LastName = "Martinez",
                Idcountries = new List<int> { 2 }
            };

            // Act
            var result = repo.updatePerson(dto);

            // Assert
            Assert.Equal("Sofia Actualizada", result.Name);
            Assert.Single(result.Countries);
            Assert.Equal("Brasil", result.Countries.First().description);
        }

        [Fact]
        public void UpdatePerson_ShouldThrow_WhenNotFound()
        {
            using var context = CreateContext();
            var repo = new personRepository(context, _mockLogger.Object);

            var dto = new updatePersonDTO
            {
                Idpersona = 999,
                Name = "No Existe",
                LastName = "Prueba",
                Idcountries = new List<int>()
            };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => repo.updatePerson(dto));
            Assert.Contains("No se encontro la persona", ex.Message);
        }
    }
}
