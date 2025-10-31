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

        public personDTO addPerson(CreateFilmDto dto)
        {
            throw new NotImplementedException();
        }

        public personDTO getPersonById(int id)
        {
            _logger.LogTrace($"Se obtuvieron los Films");
            var person =  _dbContext.Person
                    .Include(p => p.Idcountries)
                    .Where(p => p.Idpersona == id)
                    .Select(p => new personDTO
                    {
                        Idpersona = p.Idpersona,
                        Name = p.Name,
                        LastName = p.LastName,
                        Countries = p.Idcountries
                        .Select(c => new idDescriptionDTO { Id = c.Idcountry, description = c.CountryName })
                        .ToList()
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
                    .Select(p => new personDTO
                    {
                        Idpersona = p.Idpersona,
                        Name = p.Name,
                        LastName = p.LastName,
                        Countries = p.Idcountries
                        .Select(c => new idDescriptionDTO { Id = c.Idcountry, description = c.CountryName })
                        .ToList()
                    }).OrderBy(p => p.LastName).ToList();
        }

        public personDTO updatePerson(updateFilm dto)
        {
            throw new NotImplementedException();
        }
    }
}
