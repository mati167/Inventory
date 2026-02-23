using Inventory.Core.Entities.DTOs.Genre;
using Inventory.Core.Interfaces.Repository;
using Inventory.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Inventory.Core.Services
{
    public class genreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly ILogger<genreService> _log;

        public genreService(IGenreRepository genreRepository, ILogger<genreService> logger)
        {
            _genreRepository = genreRepository;
            _log = logger;
        }

        public List<GenreDto> GetGenres()
        {
            return _genreRepository.GetGenreList();
        }

        public GenreDto GetGenreById(int id)
        {
            return _genreRepository.GetGenreById(id);
        }

        public GenreDto addGenre(CreateGenreDto dto)
        {
            return _genreRepository.addGenre(dto);
        }

        public GenreDto updateGenre(updateGenreDto dto)
        {
            return _genreRepository.updateGenre(dto);
        }
    }
}
