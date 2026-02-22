using Inventory.Core.Entities.DAO;
using Inventory.Core.Entities.DTOs.Film;
using Inventory.Core.Entities.DTOs.General;
using Inventory.Core.Entities.DTOs.Person;
using Inventory.Core.Interfaces.Repository;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Repositories
{
    public class personRepository : IpersonRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly ILogger<personRepository> _logger;
        public personRepository(DatabaseContext dbContext, ILogger<personRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public personDTO addPerson(CreatePersonDTO dto)
        {
            // Traer las entidades relacionadas
            var countries = _dbContext.Countries.Where(c => dto.Idcountries.Contains(c.Idcountry)).ToList();

            // Crear la nueva persona
            var person = new Person
            {
                Name = dto.Name,
                LastName = dto.LastName,
                Idcountries = countries
            };

            _dbContext.Person.Add(person);
            _dbContext.SaveChanges(); // EF Core guarda Film y llena automáticamente las tablas intermedias

            // Opcional: devolver personDTO
            return new personDTO
            {
                Idpersona = person.Idpersona,
                Name = person.Name,
                LastName = person.LastName,
                Countries = person.Idcountries.Select(c => new idDescriptionDTO { Id = c.Idcountry, description = c.CountryName }).ToList(),
                TotalFilm = 0
            };
        }

        public personDTO getPersonById(int id)
        {
            _logger.LogTrace($"Se obtuvieron los Films");
            var person =  _dbContext.Person
                    .Include(p => p.Idcountries)
                    .Include(p => p.Idfilms)
                    .Include(p => p.IdfilmsNavigation)
                    .Where(p => p.Idpersona == id)
                    .Select(p => new personDTO
                    {
                        Idpersona = p.Idpersona,
                        Name = p.Name,
                        LastName = p.LastName,
                        Countries = p.Idcountries
                        .Select(c => new idDescriptionDTO { Id = c.Idcountry, description = c.CountryName })
                        .ToList(),
                        TotalFilm = (p.Idfilms.Count + p.IdfilmsNavigation.Count)
                    }).FirstOrDefault();
            if (person == null)
            {
                throw new Exception($"No se encontró la persona con ID {id}");
            }
            return person;
        }

        public List<personDTO> GetPersonList()
        {
            _logger.LogTrace($"Se obtuvieron los Films");
            return _dbContext.Person
                    .Include(p => p.Idcountries)
                    .Include(p => p.Idfilms)
                    .Include(p => p.IdfilmsNavigation)
                    .Select(p => new personDTO
                    {
                        Idpersona = p.Idpersona,
                        Name = p.Name,
                        LastName = p.LastName,
                        Countries = p.Idcountries
                        .Select(c => new idDescriptionDTO { Id = c.Idcountry, description = c.CountryName })
                        .ToList(),
                        TotalFilm = (p.Idfilms.Count + p.IdfilmsNavigation.Count)
                    }).OrderBy(p => p.LastName).ToList();
        }

        public personDTO updatePerson(updatePersonDTO dto)
        {
            var person = _dbContext.Person
           .Include(p => p.Idcountries)
           .Include(p => p.Idfilms)
           .Include(p => p.IdfilmsNavigation)
           .FirstOrDefault(p => p.Idpersona == dto.Idpersona);

            if (person == null)
                throw new Exception("No se encontro la persona");

            // Actualizar propiedades simples
            person.Name = dto.Name;
            person.LastName = dto.LastName;

            // Actualizar relaciones N:N
            person.Idcountries = _dbContext.Countries.Where(c => dto.Idcountries.Contains(c.Idcountry)).ToList();

            _dbContext.SaveChanges();

            // Devolver DTO actualizado
            return new personDTO
            {
                Idpersona = person.Idpersona,
                Name = person.Name,
                LastName = person.LastName,
                Countries = person.Idcountries.Select(c => new idDescriptionDTO { Id = c.Idcountry, description = c.CountryName }).ToList(),
                TotalFilm = (person.Idfilms.Count + person.IdfilmsNavigation.Count)
            };
        }
    }
}
