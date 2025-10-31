using Microsoft.AspNetCore.Mvc;
using Peliculas.Core.Interfaces;
using Peliculas.Core.Services;

namespace peliculas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmController : Controller
    {
        private readonly ILogger _log;
        private readonly IFilmService _filmService;

        public FilmController(IFilmService filmService, ILogger<FilmService> log)
        {
            _filmService = filmService;
            _log = log;
        }
        [HttpGet]
        public IActionResult GetFilms()
        {
            return Ok(_filmService.GetFilms());
        }
    }
}
