using Inventory.Core.DAO;
using Inventory.Core.DTOs.Film;
using Inventory.Core.DTOs.General;
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
        public List<FilmDto> GetFilmList()
        {
            _logger.LogTrace($"Se obtuvieron los Films");
            return _dbContext.Films
                    .Include(f => f.Idcountries)
                    .Include(f => f.Idgenres)
                    .Select(f => new FilmDto
                    {
                        Idfilm = f.Idfilm,
                        FilmName = f.FilmName,
                        Year = f.Year,
                        Duration = f.Duration,
                        Directed = f.Idpeople
                        .Select(d => new idDescriptionDTO { Id = d.Idpersona, description = d.LastName + "," + d.Name })
                        .ToList(),
                        Countries = f.Idcountries
                        .Select(c => new idDescriptionDTO { Id = c.Idcountry, description = c.CountryName })
                        .ToList(),
                        Genres = f.Idgenres
                        .Select(g => new idDescriptionDTO { Id = g.Idgenre, description = g.Description ?? "S/D" })
                        .ToList(),
                    }).OrderBy(f => f.FilmName).ToList();
        }
        public FilmDto GetFilmById(int id)
        {
            _logger.LogTrace($"Se obtuvo el Film");
            var film = _dbContext.Films
                   .Include(f => f.Idcountries)
                   .Include(f => f.Idgenres)
                   .Where(f => f.Idfilm == id)
                   .Select(f => new FilmDto                   
                   {
                       Idfilm = f.Idfilm,
                       FilmName = f.FilmName,
                       Year = f.Year,
                       Duration = f.Duration,
                       Directed = f.Idpeople
                       .Select(d => new idDescriptionDTO { Id = d.Idpersona, description = d.LastName + "," + d.Name })
                       .ToList(),
                       Countries = f.Idcountries
                       .Select(c => new idDescriptionDTO { Id = c.Idcountry, description = c.CountryName })
                       .ToList(),
                       Genres = f.Idgenres
                       .Select(g => new idDescriptionDTO { Id = g.Idgenre, description = g.Description ?? "S/D" })
                       .ToList(),
                   }).FirstOrDefault();
            if(film == null)
            {
                throw new Exception($"No se encontró la película con ID {id}");
            }
            return film;

        }
    }
}
