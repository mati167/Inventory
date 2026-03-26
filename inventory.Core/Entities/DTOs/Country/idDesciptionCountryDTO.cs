using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Core.Entities.DTOs.Country
{
    public  class idDesciptionCountryDTO
    {
        public int Id { get; set; }
        public string description { get; set; } = null!;
        public string isoCode { get; set; } = null!;
    }
}
