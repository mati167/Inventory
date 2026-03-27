using Inventory.Core.Entities.DTOs.Login;
using Inventory.Core.Interfaces.Repository;
using Inventory.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Core.Services
{
    public class loginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ILogger<genreService> _log;

        public loginService(ILoginRepository loginRepository, ILogger<genreService> logger)
        {
            _loginRepository = loginRepository;
            _log = logger;
        }
        public async Task<bool> LoginAsync(loginDTO login)
        {
            try
            {
                var res = await _loginRepository.getUsernameAsync(login.UserName);
                if (res == null)
                    return false;
                bool esValido = BCrypt.Net.BCrypt.Verify(login.Password, res.PasswordHash);

                return esValido;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "An error occurred while trying to log in.");
                throw;
            }
        }
    }
}