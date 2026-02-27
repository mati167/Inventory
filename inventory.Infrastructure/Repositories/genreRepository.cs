using Inventory.Core.Entities.DAO;
using Inventory.Core.Entities.DTOs.Genre;
using Inventory.Core.Interfaces.Repository;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.Infrastructure.Repositories
{
    public class genreRepository : IGenreRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly ILogger<genreRepository> _logger;
        public genreRepository(DatabaseContext dbContext, ILogger<genreRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public List<GenreDto> GetGenreList()
        {
            _logger.LogTrace("Se obtuvieron los Genres");
            return _dbContext.Genres
                .Include(g => g.idfilms)
                .Select(g => new GenreDto
                {
                    Idgenre = g.idgenre,
                    Description = g.description,
                    TotalFilm = g.idfilms.Count
                }).OrderBy(g => g.Description).ToList();
        }

        public GenreDto GetGenreById(int id)
        {
            _logger.LogTrace("Se obtuvo el Genre");
            var genre = _dbContext.Genres
                .Include(g => g.idfilms)
                .Where(g => g.idgenre == id)
                .Select(g => new GenreDto
                {
                    Idgenre = g.idgenre,
                    Description = g.description,
                    TotalFilm = g.idfilms.Count
                }).FirstOrDefault();

            if (genre == null)
                throw new Exception($"No se encontró el genre con ID {id}");

            return genre;
        }

        public GenreDto addGenre(CreateGenreDto dto)
        {
            var genre = new Genre
            {
                description = dto.Description
            };

            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();

            return new GenreDto
            {
                Idgenre = genre.idgenre,
                Description = genre.description,
                TotalFilm = 0
            };
        }

        public GenreDto updateGenre(updateGenreDto dto)
        {
            var genre = _dbContext.Genres
                .Include(g => g.idfilms)
                .FirstOrDefault(g => g.idgenre == dto.Idgenre);
            if (genre == null)
                throw new Exception("No se encontro el genre");

            genre.description = dto.Description;

            _dbContext.SaveChanges();

            return new GenreDto
            {
                Idgenre = genre.idgenre,
                Description = genre.description,
                TotalFilm = genre.idfilms.Count
            };
        }
    }
}
