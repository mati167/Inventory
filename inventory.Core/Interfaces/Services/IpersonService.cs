using Inventory.Core.Entities.DTOs.Film;
using Inventory.Core.Entities.DTOs.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Interfaces.Services
{
    public interface IpersonService
    {
        List<personDTO> GetPersonList();
        personDTO GetPersonById(int id);
        personDTO addPerson(CreatePersonDTO dto);
        personDTO updatePerson(updatePersonDTO dto);
    }
}
