using Inventory.Core.Entities.DTOs.Country;
using Inventory.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Inventory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly ILogger<CountryController> _log;

        public CountryController(ICountryService countryService, ILogger<CountryController> log)
        {
            _countryService = countryService;
            _log = log;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<CountryDto>))]
        [HttpGet("GetCountries")]
        public IActionResult GetCountries()
        {
            return Ok(_countryService.GetCountries());
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CountryDto))]
        [HttpGet("GetCountryById")]
        public IActionResult GetCountryById(int id)
        {
            return Ok(_countryService.GetCountryById(id));
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CountryDto))]
        [HttpPost("addCountry")]
        public IActionResult addCountry(CreateCountryDto dto)
        {
            return Ok(_countryService.addCountry(dto));
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CountryDto))]
        [HttpPut("updateCountry")]
        public IActionResult updateCountry(updateCountryDto dto)
        {
            return Ok(_countryService.updateCountry(dto));
        }
    }
}
