using Inventory.Core.Entities.DTOs.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Entities.DTOs.Person
{
    public class personDTO
    {
        public int Idpersona { get; set; }

        public string? Name { get; set; }

        public string? LastName { get; set; }
        public List<idDescriptionDTO> Countries { get; set; } = new();
    }
}
