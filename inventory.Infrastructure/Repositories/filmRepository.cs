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
                        Directed = f.idDirected
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
                       Directed = f.idDirected
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

        public FilmDto addFilm(CreateFilmDto dto)
        {
            // Traer las entidades relacionadas
            var countries = _dbContext.Countries.Where(c => dto.CountryIds.Contains(c.Idcountry)).ToList();
            var genres = _dbContext.Genres.Where(g => dto.GenreIds.Contains(g.Idgenre)).ToList();
            var peopleDirected = _dbContext.People.Where(p => dto.DirectedIds.Contains(p.Idpersona)).ToList();
            var peopleActed = _dbContext.People.Where(p => dto.ActedIds.Contains(p.Idpersona)).ToList();

            // Crear la nueva película
            var film = new Film
            {
                FilmName = dto.FilmName,
                Year = dto.Year,
                Duration = dto.Duration,
                Idcountries = countries,
                Idgenres = genres,
                idDirected = peopleDirected,
                idActed = peopleActed
            };

            _dbContext.Films.Add(film);
            _dbContext.SaveChanges(); // EF Core guarda Film y llena automáticamente las tablas intermedias

            // Opcional: devolver FilmDto
            return new FilmDto
            {
                Idfilm = film.Idfilm,
                FilmName = film.FilmName,
                Year = film.Year,
                Duration = film.Duration,
                Countries = film.Idcountries.Select(c => new idDescriptionDTO { Id = c.Idcountry, description = c.CountryName }).ToList(),
                Genres = film.Idgenres.Select(g => new idDescriptionDTO { Id = g.Idgenre, description = g.Description ?? "S/D" }).ToList(),
                Directed = film.idDirected.Select(p => new idDescriptionDTO { Id = p.Idpersona, description = p.LastName + "," + p.Name }).ToList(),
                Acted = film.idActed.Select(p => new idDescriptionDTO { Id = p.Idpersona, description = p.LastName + "," + p.Name }).ToList()
            };
        }
    }
}
