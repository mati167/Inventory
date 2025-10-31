using Inventory.Core.DTOs.Film;
using Microsoft.AspNetCore.Mvc;
using Peliculas.Core.Interfaces;
using Peliculas.Core.Services;
using System.Net;

namespace peliculas.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmController : Controller
    {
        private readonly ILogger _log;
        private readonly IFilmService _filmService;

        public FilmController(IFilmService filmService, ILogger<FilmService> log)
        {
            _filmService = filmService;
            _log = log;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<FilmDto>))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(List<ErrorDetalle>))]
        [HttpGet("GetFilms")]
        public IActionResult GetFilms()
        {
            return Ok(_filmService.GetFilms());
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(FilmDto))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(List<ErrorDetalle>))]
        [HttpGet("GetFilmById")]
        public IActionResult GetFilmById(int id)
        {
            return Ok(_filmService.GetFilmById(id));
        }
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(FilmDto))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(List<ErrorDetalle>))]
        [HttpPost("addFilm")]
        public IActionResult addFilm(CreateFilmDto dto)
        {
            return Ok(_filmService.addFilm(dto));
        }
    }
}
