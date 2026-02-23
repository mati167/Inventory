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

        [Fact]
        public void AddPerson_ShouldAddPersonWithMultipleCountries()
        {
            // Arrange
            using var context = CreateContext();

            var country1 = new Country { Idcountry = 1, CountryName = "Argentina" };
            var country2 = new Country { Idcountry = 2, CountryName = "Chile" };
            context.Countries.AddRange(country1, country2);
            context.SaveChanges();

            var repo = new personRepository(context, _mockLogger.Object);

            var dto = new CreatePersonDTO
            {
                Name = "Carlos",
                LastName = "López",
                Idcountries = new List<int> { 1, 2 }
            };

            // Act
            var result = repo.addPerson(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Carlos", result.Name);
            Assert.Equal("López", result.LastName);
            Assert.Equal(2, result.Countries.Count);
            Assert.Contains(result.Countries, c => c.description == "Argentina");
            Assert.Contains(result.Countries, c => c.description == "Chile");
        }

        [Fact]
        public void AddPerson_ShouldSetTotalFilmToZero()
        {
            // Arrange
            using var context = CreateContext();

            var country = new Country { Idcountry = 1, CountryName = "España" };
            context.Countries.Add(country);
            context.SaveChanges();

            var repo = new personRepository(context, _mockLogger.Object);

            var dto = new CreatePersonDTO
            {
                Name = "Miguel",
                LastName = "García",
                Idcountries = new List<int> { 1 }
            };

            // Act
            var result = repo.addPerson(dto);

            // Assert
            Assert.Equal(0, result.TotalFilm);
        }

        [Fact]
        public void GetPersonById_ShouldCalculateTotalFilmCorrectly()
        {
            // Arrange
            using var context = CreateContext();

            var country = new Country { Idcountry = 1, CountryName = "México" };
            var person = new Person
            {
                Idpersona = 5,
                Name = "Roberto",
                LastName = "Fernández",
                Idcountries = new List<Country> { country },
                Idfilms = new List<Film>(),
                IdfilmsNavigation = new List<Film>()
            };

            context.Countries.Add(country);
            context.Person.Add(person);
            context.SaveChanges();

            var repo = new personRepository(context, _mockLogger.Object);

            // Act
            var result = repo.getPersonById(5);

            // Assert
            Assert.Equal(0, result.TotalFilm);
        }

        [Fact]
        public void GetPersonList_ShouldReturnEmptyList_WhenNoPersonsExist()
        {
            // Arrange
            using var context = CreateContext();
            var repo = new personRepository(context, _mockLogger.Object);

            // Act
            var result = repo.GetPersonList();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetPersonList_ShouldReturnAllPersonsOrderedByLastName()
        {
            // Arrange
            using var context = CreateContext();

            var country = new Country { Idcountry = 1, CountryName = "Perú" };
            var person1 = new Person { Idpersona = 1, Name = "Juan", LastName = "Zorro", Idcountries = new List<Country> { country } };
            var person2 = new Person { Idpersona = 2, Name = "María", LastName = "Antúnez", Idcountries = new List<Country> { country } };
            var person3 = new Person { Idpersona = 3, Name = "Pablo", LastName = "Morales", Idcountries = new List<Country> { country } };

            context.Countries.Add(country);
            context.Person.AddRange(person1, person2, person3);
            context.SaveChanges();

            var repo = new personRepository(context, _mockLogger.Object);

            // Act
            var result = repo.GetPersonList();

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal("Antúnez", result[0].LastName);
            Assert.Equal("Morales", result[1].LastName);
            Assert.Equal("Zorro", result[2].LastName);
        }

        [Fact]
        public void UpdatePerson_ShouldRemoveCountriesAndAddNew()
        {
            // Arrange
            using var context = CreateContext();

            var country1 = new Country { Idcountry = 1, CountryName = "Portugal" };
            var country2 = new Country { Idcountry = 2, CountryName = "Italia" };
            var country3 = new Country { Idcountry = 3, CountryName = "Francia" };

            var person = new Person
            {
                Idpersona = 2,
                Name = "Alessandro",
                LastName = "Rossi",
                Idcountries = new List<Country> { country1, country2 }
            };

            context.Countries.AddRange(country1, country2, country3);
            context.Person.Add(person);
            context.SaveChanges();

            var repo = new personRepository(context, _mockLogger.Object);

            var dto = new updatePersonDTO
            {
                Idpersona = 2,
                Name = "Alessandro",
                LastName = "Rossi",
                Idcountries = new List<int> { 3 }
            };

            // Act
            var result = repo.updatePerson(dto);

            // Assert
            Assert.Single(result.Countries);
            Assert.Equal("Francia", result.Countries.First().description);
        }

        [Fact]
        public void UpdatePerson_ShouldUpdateOnlyName()
        {
            // Arrange
            using var context = CreateContext();

            var country = new Country { Idcountry = 1, CountryName = "Grecia" };
            var person = new Person
            {
                Idpersona = 3,
                Name = "Nicolás",
                LastName = "Papadopoulos",
                Idcountries = new List<Country> { country }
            };

            context.Countries.Add(country);
            context.Person.Add(person);
            context.SaveChanges();

            var repo = new personRepository(context, _mockLogger.Object);

            var dto = new updatePersonDTO
            {
                Idpersona = 3,
                Name = "Nicolás Actualizado",
                LastName = "Papadopoulos",
                Idcountries = new List<int> { 1 }
            };

            // Act
            var result = repo.updatePerson(dto);

            // Assert
            Assert.Equal("Nicolás Actualizado", result.Name);
            Assert.Equal("Papadopoulos", result.LastName);
            Assert.Single(result.Countries);
        }

        [Fact]
        public void UpdatePerson_ShouldKeepFilmAssociations()
        {
            // Arrange
            using var context = CreateContext();

            var country1 = new Country { Idcountry = 1, CountryName = "Suecia" };
            var country2 = new Country { Idcountry = 2, CountryName = "Noruega" };

            var person = new Person
            {
                Idpersona = 4,
                Name = "Ingrid",
                LastName = "Svensson",
                Idcountries = new List<Country> { country1 },
                Idfilms = new List<Film>(),
                IdfilmsNavigation = new List<Film>()
            };

            context.Countries.AddRange(country1, country2);
            context.Person.Add(person);
            context.SaveChanges();

            var repo = new personRepository(context, _mockLogger.Object);

            var dto = new updatePersonDTO
            {
                Idpersona = 4,
                Name = "Ingrid",
                LastName = "Svensson",
                Idcountries = new List<int> { 2 }
            };

            // Act
            var result = repo.updatePerson(dto);

            // Assert
            Assert.Equal(0, result.TotalFilm);
        }

        [Fact]
        public void AddPerson_ShouldReturnCorrectPersonDTO()
        {
            // Arrange
            using var context = CreateContext();

            var country = new Country { Idcountry = 1, CountryName = "Canadá" };
            context.Countries.Add(country);
            context.SaveChanges();

            var repo = new personRepository(context, _mockLogger.Object);

            var dto = new CreatePersonDTO
            {
                Name = "James",
                LastName = "Smith",
                Idcountries = new List<int> { 1 }
            };

            // Act
            var result = repo.addPerson(dto);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(0, result.Idpersona);
            Assert.Equal("James", result.Name);
            Assert.Equal("Smith", result.LastName);
            Assert.Single(result.Countries);
            Assert.Equal(1, result.Countries.First().Id);
        }

        [Fact]
        public void GetPersonById_ShouldIncludeAllCountries()
        {
            // Arrange
            using var context = CreateContext();

            var country1 = new Country { Idcountry = 1, CountryName = "Suiza" };
            var country2 = new Country { Idcountry = 2, CountryName = "Austria" };
            var country3 = new Country { Idcountry = 3, CountryName = "Alemania" };

            var person = new Person
            {
                Idpersona = 10,
                Name = "Hans",
                LastName = "Mueller",
                Idcountries = new List<Country> { country1, country2, country3 }
            };

            context.Countries.AddRange(country1, country2, country3);
            context.Person.Add(person);
            context.SaveChanges();

            var repo = new personRepository(context, _mockLogger.Object);

            // Act
            var result = repo.getPersonById(10);

            // Assert
            Assert.Equal(3, result.Countries.Count);
            Assert.Contains(result.Countries, c => c.description == "Suiza");
            Assert.Contains(result.Countries, c => c.description == "Austria");
            Assert.Contains(result.Countries, c => c.description == "Alemania");
        }
    }
}
