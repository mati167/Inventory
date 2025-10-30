using Microsoft.AspNetCore.Mvc;

namespace peliculas.Api.Controllers
{
    public class FilmController : Controller
    {
        [HttpGet]
        public IActionResult GetFilms()
        {
            return Ok(GetFilms());
        }
    }
}
