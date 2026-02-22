using Inventory.Core.Entities.DTOs.Film;
using Inventory.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Peliculas.Core.Services;
using System.Net;

namespace Inventory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class generalController :Controller
    {
        private readonly ILogger _log;
        private readonly IFilmService _filmService;

        public generalController(IFilmService filmService, ILogger<FilmService> log)
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
    }
}
