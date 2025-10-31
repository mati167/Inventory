using Inventory.Core.Entities.DTOs.Film;
using Inventory.Core.Entities.DTOs.Person;
using Inventory.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Services
{
    public class personService : IpersonService
    {
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
            throw new NotImplementedException();
        }

        public personDTO updatePerson(updateFilm dto)
        {
            throw new NotImplementedException();
        }
    }
}
