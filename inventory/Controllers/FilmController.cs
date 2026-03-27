using Inventory.Core.Entities.DTOs.Film;
using Inventory.Core.Interfaces.Services;
using Inventory.Core.Interfaces.Gateway;
using Inventory.Core.Entities.DTOs.OMDb;
using Microsoft.AspNetCore.Mvc;
using Peliculas.Core.Services;
using System.Net;
using System.Text.Json;

namespace peliculas.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmController : Controller
    {
        private readonly ILogger _log;
        private readonly IFilmService _filmService;
        private readonly IimdbGateway _imdbGateway;

        public FilmController(IFilmService filmService, ILogger<FilmService> log, IimdbGateway imdbGateway)
        {
            _filmService = filmService;
            _log = log;
            _imdbGateway = imdbGateway;
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

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(movieResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("GetMovieFromOmdb")]
        public async Task<IActionResult> GetMovieFromOmdb(string imdbId)
        {
            if (string.IsNullOrWhiteSpace(imdbId))
            {
                return BadRequest("El imdbId es requerido");
            }

            try
            {
                var movie = await _imdbGateway.getMovie(imdbId);

                if (movie == null)
                {
                    return NotFound($"No se encontró película con IMDb ID: {imdbId}");
                }

                return Ok(movie);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, $"Error al obtener película de OMDb con ID: {imdbId}");
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error al obtener datos de OMDb: {ex.Message}");
            }
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(FilmDto))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(List<ErrorDetalle>))]
        [HttpPost("addFilm")]
        public IActionResult addFilm(CreateFilmDto dto)
        {
            _log.LogInformation("addFilm Body: ", JsonSerializer.Serialize(dto));
            return Ok(_filmService.addFilm(dto));
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(FilmDto))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(List<ErrorDetalle>))]
        [HttpPut("updateFilm")]
        public IActionResult updateFilm(updateFilm dto)
        {
            return Ok(_filmService.updateFilm(dto));
        }
    }
}
