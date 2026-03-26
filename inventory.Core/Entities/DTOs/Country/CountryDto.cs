using Inventory.Core.Entities.DTOs.General;

namespace Inventory.Core.Entities.DTOs.Country
{
    public class CountryDto
    {
        public int Idcountry { get; set; }
        public string CountryName { get; set; } = null!;
        public string? isoCode { get; set; } = null!;
        public idDescriptionDTO? Continent { get; set; }
        public int TotalFilm { get; set; }
        public int TotalPerson { get; set; }
    }
}
