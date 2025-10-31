using Inventory.Core.Entities.DTOs.Film;
using Inventory.Core.Entities.DTOs.Person;
using Inventory.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Peliculas.Core.Services;
using System.Net;

namespace Inventory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class personController : Controller
    {
        private readonly ILogger _log;
        private readonly IpersonService _personService;
        public personController(IpersonService personService, ILogger<personController> log)
        {
            _personService = personService;
            _log = log;
        }
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<personDTO>))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(List<ErrorDetalle>))]
        [HttpGet("GetPersonList")]
        public IActionResult GetPersonList()
        {
            return Ok(_personService.GetPersonList());
        }
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<personDTO>))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(List<ErrorDetalle>))]
        [HttpGet("GetPersonById")]
        public IActionResult GetPersonById(int id)
        {
            return Ok(_personService.GetPersonById(id));
        }
    }
}
