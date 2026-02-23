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
                .Include(g => g.Idfilms)
                .Select(g => new GenreDto
                {
                    Idgenre = g.Idgenre,
                    Description = g.Description,
                    TotalFilm = g.Idfilms.Count
                }).OrderBy(g => g.Description).ToList();
        }

        public GenreDto GetGenreById(int id)
        {
            _logger.LogTrace("Se obtuvo el Genre");
            var genre = _dbContext.Genres
                .Include(g => g.Idfilms)
                .Where(g => g.Idgenre == id)
                .Select(g => new GenreDto
                {
                    Idgenre = g.Idgenre,
                    Description = g.Description,
                    TotalFilm = g.Idfilms.Count
                }).FirstOrDefault();

            if (genre == null)
                throw new Exception($"No se encontró el genre con ID {id}");

            return genre;
        }

        public GenreDto addGenre(CreateGenreDto dto)
        {
            var genre = new Genre
            {
                Description = dto.Description
            };

            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();

            return new GenreDto
            {
                Idgenre = genre.Idgenre,
                Description = genre.Description,
                TotalFilm = 0
            };
        }

        public GenreDto updateGenre(updateGenreDto dto)
        {
            var genre = _dbContext.Genres
                .Include(g => g.Idfilms)
                .FirstOrDefault(g => g.Idgenre == dto.Idgenre);
            if (genre == null)
                throw new Exception("No se encontro el genre");

            genre.Description = dto.Description;

            _dbContext.SaveChanges();

            return new GenreDto
            {
                Idgenre = genre.Idgenre,
                Description = genre.Description,
                TotalFilm = genre.Idfilms.Count
            };
        }
    }
}
