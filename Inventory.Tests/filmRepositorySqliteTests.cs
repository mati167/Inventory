using Xunit;
using Inventory.Core.DTOs.Film;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Inventory.Tests
{
    public class filmRepositorySqliteTests : IClassFixture<filmRepositoryFixture>
    {
        private readonly filmRepositoryFixture _fixture;

        public filmRepositorySqliteTests(filmRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void GetFilmList_ShouldReturnAllFilms()
        {
            var result = _fixture.Repository.GetFilmList();

            Assert.Single(result);
            var film = result.First();
            Assert.Equal("El Secreto de Sus Ojos", film.FilmName);
            Assert.Equal("Argentina", film.Countries.First().description);
            Assert.Equal("Drama", film.Genres.First().description);
        }

        [Fact]
        public void GetFilmById_ShouldReturnCorrectFilm()
        {
            var result = _fixture.Repository.GetFilmById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Idfilm);
            Assert.Equal("El Secreto de Sus Ojos", result.FilmName);
        }

        [Fact]
        public void GetFilmById_ShouldThrowException_WhenNotFound()
        {
            var ex = Assert.Throws<Exception>(() => _fixture.Repository.GetFilmById(999));
            Assert.Contains("No se encontró la película", ex.Message);
        }

        [Fact]
        public void AddFilm_ShouldInsertFilmCorrectly()
        {
            var dto = new CreateFilmDto
            {
                FilmName = "Nueva Película",
                Year = 2024,
                Duration = TimeOnly.Parse("02:00"),
                CountryIds = new List<int> { 1 },
                GenreIds = new List<int> { 1 },
                DirectedIds = new List<int> { 1 },
                ActedIds = new List<int> { 2 }
            };

            var result = _fixture.Repository.addFilm(dto);

            Assert.NotNull(result);
            Assert.Equal("Nueva Película", result.FilmName);
            Assert.True(_fixture.Context.Films.Count() >= 2);
        }

        [Fact]
        public void UpdateFilm_ShouldModifyExistingFilm()
        {
            var dto = new updateFilm
            {
                Idfilm = 1,
                FilmName = "Película Actualizada",
                Year = 2010,
                Duration = TimeOnly.Parse("02:30"),
                CountryIds = new List<int> { 1 },
                GenreIds = new List<int> { 1 },
                DirectedIds = new List<int> { 1 },
                ActedIds = new List<int> { 2 }
            };

            var result = _fixture.Repository.updateFilm(dto);

            Assert.NotNull(result);
            Assert.Equal("Película Actualizada", result.FilmName);
            Assert.Equal(TimeOnly.Parse("02:30"), result.Duration);
        }
    }
}
