using Inventory.Core.Entities.DAO;
using Inventory.Core.Entities.DTOs.General;
using Inventory.Core.Entities.DTOs.Film;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Peliculas.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Core.Interfaces.Repository;

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
                    .Include(f => f.idcountries)
                    .Include(f => f.idgenres)
                    .Select(f => new FilmDto
                    {
                        Idfilm = f.idfilm,
                        FilmName = f.filmname,
                        Year = f.year,
                        Duration = f.duration,
                        imdbID = f.imdbid,
                        Directed = f.iddirected
                        .Select(d => new idDescriptionDTO { Id = d.idpersona, description = d.lastname + "," + d.name })
                        .ToList(),
                        Countries = f.idcountries
                        .Select(c => new idDescriptionDTO { Id = c.idcountry, description = c.countryname })
                        .ToList(),
                        Genres = f.idgenres
                        .Select(g => new idDescriptionDTO { Id = g.idgenre, description = g.description ?? "S/D" })
                        .ToList(),
                    }).OrderBy(f => f.FilmName).ToList();
        }
        public FilmDto GetFilmById(int id)
        {
            _logger.LogTrace($"Se obtuvo el Film");
            var film = _dbContext.Films
                   .Include(f => f.idcountries)
                   .Include(f => f.idgenres)
                   .Where(f => f.idfilm == id)
                   .Select(f => new FilmDto                   
                   {
                       Idfilm = f.idfilm,
                       FilmName = f.filmname,
                       Year = f.year,
                       Duration = f.duration,
                       imdbID = f.imdbid,
                       Directed = f.iddirected
                       .Select(d => new idDescriptionDTO { Id = d.idpersona, description = d.lastname + "," + d.name })
                       .ToList(),
                       Countries = f.idcountries
                       .Select(c => new idDescriptionDTO { Id = c.idcountry, description = c.countryname })
                       .ToList(),
                       Genres = f.idgenres
                       .Select(g => new idDescriptionDTO { Id = g.idgenre, description = g.description ?? "S/D" })
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
            var countries = _dbContext.Countries.Where(c => dto.CountryIds.Contains(c.idcountry)).ToList();
            var genres = _dbContext.Genres.Where(g => dto.GenreIds.Contains(g.idgenre)).ToList();
            var peopleDirected = _dbContext.Person.Where(p => dto.DirectedIds.Contains(p.idpersona)).ToList();
            var peopleActed = _dbContext.Person.Where(p => dto.ActedIds.Contains(p.idpersona)).ToList();

            // Crear la nueva película
            var film = new Film
            {
                filmname = dto.FilmName,
                year = dto.Year,
                duration = dto.Duration,
                imdbid = dto.imdbID,
                idcountries = countries,
                idgenres = genres,
                iddirected = peopleDirected,
                idacted = peopleActed
            };

            _dbContext.Films.Add(film);
            _dbContext.SaveChanges(); // EF Core guarda Film y llena automáticamente las tablas intermedias

            // Opcional: devolver FilmDto
            return new FilmDto
            {
                Idfilm = film.idfilm,
                FilmName = film.filmname,
                Year = film.year,
                Duration = film.duration,
                imdbID = film.imdbid,
                Countries = film.idcountries.Select(c => new idDescriptionDTO { Id = c.idcountry, description = c.countryname }).ToList(),
                Genres = film.idgenres.Select(g => new idDescriptionDTO { Id = g.idgenre, description = g.description ?? "S/D" }).ToList(),
                Directed = film.iddirected.Select(p => new idDescriptionDTO { Id = p.idpersona, description = p.lastname + "," + p.name }).ToList(),
                Acted = film.idacted.Select(p => new idDescriptionDTO { Id = p.idpersona, description = p.lastname + "," + p.name }).ToList()
            };
        }

        public FilmDto updateFilm(updateFilm dto)
        {
            var film = _dbContext.Films
                       .Include(f => f.idcountries)
                       .Include(f => f.idgenres)
                       .Include(f => f.iddirected)
                       .Include(f => f.idacted)
                       .FirstOrDefault(f => f.idfilm == dto.Idfilm);

            if (film == null)
                throw new Exception("No se encontro el film");

            // Actualizar propiedades simples
            film.filmname = dto.FilmName;
            film.year = dto.Year;
            film.duration = dto.Duration;
            film.imdbid = dto.imdbID;

            // Actualizar relaciones N:N
            film.idcountries = _dbContext.Countries.Where(c => dto.CountryIds.Contains(c.idcountry)).ToList();
            film.idgenres = _dbContext.Genres.Where(g => dto.GenreIds.Contains(g.idgenre)).ToList();
            film.iddirected = _dbContext.Person.Where(p => dto.DirectedIds.Contains(p.idpersona)).ToList();
            film.idacted = _dbContext.Person.Where(p => dto.ActedIds.Contains(p.idpersona)).ToList();

            _dbContext.SaveChanges();

            // Devolver DTO actualizado
            return new FilmDto
            {
                Idfilm = film.idfilm,
                FilmName = film.filmname,
                Year = film.year,
                Duration = film.duration,
                imdbID = film.imdbid,
                Countries = film.idcountries.Select(c => new idDescriptionDTO { Id = c.idcountry, description = c.countryname }).ToList(),
                Genres = film.idgenres.Select(g => new idDescriptionDTO { Id = g.idgenre, description = g.description ?? "S/D" }).ToList(),
                Directed = film.iddirected.Select(p => new idDescriptionDTO { Id = p.idpersona, description = p.lastname + "," + p.name }).ToList(),
                Acted = film.idacted.Select(p => new idDescriptionDTO { Id = p.idpersona, description = p.lastname + "," + p.name }).ToList()
            };
        }
    }
}
