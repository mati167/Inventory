using Inventory.Core.Entities.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Entities.DTOs.Person
{
    public class CreatePersonDTO
    {
        public string? Name { get; set; }

        public string? LastName { get; set; }

        public  List<int> Idcountries { get; set; } = new();
    }
}
