using Inventory.Core.Entities.DTOs.Genre;
using Inventory.Core.Entities.DTOs.Login;
using Inventory.Core.Interfaces.Services;
using Inventory.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Inventory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogger _log;
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService, ILogger<personController> log)
        {
            _loginService = loginService;
            _log = log;
        }
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(loginResponse))]
        [HttpGet("login")]
        public IActionResult Login(loginDTO login)
        {
            return Ok(_loginService.LoginAsync(login));
        }
    }
}
