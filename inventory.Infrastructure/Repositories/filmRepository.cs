using Inventory.Core.DAO;
using Inventory.Core.Interfaces;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Peliculas.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Repositories
{
    public class filmRepository : IFilmRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly ILogger<filmRepository> _logger;
        public filmRepository(DatabaseContext dbContext, ILogger<filmRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public List<Film> GetFilmList()
        {
            _logger.LogTrace($"Se obtuvieron los Films");
            return _dbContext.Films
                    .Include(f => f.Idcountries)
                    .Include(f => f.Idgenres)
                    .Include(f => f.Idpeople)
                    .Include(f => f.IdpeopleNavigation).OrderBy(f => f.FilmName).ToList();
        }
    }
}
