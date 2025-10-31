using Inventory.Core.Entities.DTOs.Film;
using Inventory.Core.Entities.DTOs.Person;
using Inventory.Core.Interfaces.Repository;
using Inventory.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Peliculas.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Services
{
    public class personService : IpersonService
    {
        private readonly IpersonService _personRepository;
        private readonly ILogger<personService> _log;

        public personService(IpersonService personRepository, ILogger<personService> logger)
        {
            _personRepository = personRepository;
            _log = logger;
        }
        public personDTO addPerson(CreateFilmDto dto)
        {
            throw new NotImplementedException();
        }

        public personDTO GetPersonById(int id)
        {
            throw new NotImplementedException();
        }

        public List<personDTO> GetPersonList()
        {
            return _personRepository.GetPersonList();
        }

        public personDTO updatePerson(updateFilm dto)
        {
            throw new NotImplementedException();
        }
    }
}
