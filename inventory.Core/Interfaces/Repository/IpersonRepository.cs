using Inventory.Core.Entities.DTOs.Film;
using Inventory.Core.Entities.DTOs.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Interfaces.Repository
{
    public interface IpersonRepository
    {
        List<personDTO> GetPersonList();
        personDTO getPersonById(int id);
        personDTO addPerson(CreateFilmDto dto);
        personDTO updatePerson(updateFilm dto);
    }
}
