using Inventory.Core.DAO;
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
        public List<Film> GetFilms()
        {
            return _filmRepository.GetFilmList();
        }
    }
}
