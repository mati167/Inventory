using Inventory.Core.DAO;
using Inventory.Core.DTOs.Film;
using Inventory.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Peliculas.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.Core.Services
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly ILogger<FilmService> _log;

        public FilmService(IFilmRepository filmRepository,ILogger<FilmService> logger)
        {
            _filmRepository = filmRepository;
            _log = logger;
        }
        public List<FilmDto> GetFilms()
        {
            return _filmRepository.GetFilmList();
        }

        public FilmDto GetFilmById(int id)
        {
            return _filmRepository.GetFilmById(id);
        }
        public FilmDto addFilm(CreateFilmDto dto)
        {
            return _filmRepository.addFilm(dto);
        }

        public FilmDto updateFilm(updateFilm dto)
        {
            return _filmRepository.updateFilm(dto);
        }
    }
}
