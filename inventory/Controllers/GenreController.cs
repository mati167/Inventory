using Inventory.Core.Entities.DTOs.Genre;
using Inventory.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Inventory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly ILogger<GenreController> _log;

        public GenreController(IGenreService genreService, ILogger<GenreController> log)
        {
            _genreService = genreService;
            _log = log;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<GenreDto>))]
        [HttpGet("GetGenres")]
        public IActionResult GetGenres()
        {
            return Ok(_genreService.GetGenres());
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenreDto))]
        [HttpGet("GetGenreById")]
        public IActionResult GetGenreById(int id)
        {
            return Ok(_genreService.GetGenreById(id));
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenreDto))]
        [HttpPost("addGenre")]
        public IActionResult addGenre(CreateGenreDto dto)
        {
            return Ok(_genreService.addGenre(dto));
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenreDto))]
        [HttpPut("updateGenre")]
        public IActionResult updateGenre(updateGenreDto dto)
        {
            return Ok(_genreService.updateGenre(dto));
        }
    }
}
