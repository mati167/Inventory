using Inventory.Core.Entities.DAO;
using Inventory.Core.Entities.DTOs.Genre;
using Inventory.Core.Interfaces.Repository;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.Infrastructure.Repositories
{
    public class loginRepository : ILoginRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly ILogger<loginRepository> _logger;
        public loginRepository(DatabaseContext dbContext, ILogger<loginRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public Task<Administrator?> getUsernameAsync(string username)
        {
            _logger.LogTrace("Se busca el user");
            return _dbContext.Administrator.FirstOrDefaultAsync(a => a.Username == username);
        }

    }
}
