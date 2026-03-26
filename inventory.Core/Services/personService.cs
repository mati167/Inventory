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
        private readonly IpersonRepository _personRepository;
        private readonly ILogger<personService> _log;

        public personService(IpersonRepository personRepository, ILogger<personService> logger)
        {
            _personRepository = personRepository;
            _log = logger;
        }
        public personDTO addPerson(CreatePersonDTO dto)
        {
            return _personRepository.addPerson(dto);
        }

        public personDTO GetPersonById(int id)
        {
            return _personRepository.getPersonById(id);
        }

        public List<personDTO> GetPersonList()
        {
            return _personRepository.GetPersonList();
        }

        public personDTO updatePerson(updatePersonDTO dto)
        {
            return _personRepository.updatePerson(dto);
        }
    }
}
