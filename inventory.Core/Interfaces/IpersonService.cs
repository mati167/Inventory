using Inventory.Core.Entities.DTOs.Film;
using Inventory.Core.Entities.DTOs.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Interfaces
{
    public interface IpersonService
    {
        List<personDTO> GetPersonList();
        personDTO GetPersonById(int id);
        personDTO addPerson(CreateFilmDto dto);
        personDTO updatePerson(updateFilm dto);
    }
}
