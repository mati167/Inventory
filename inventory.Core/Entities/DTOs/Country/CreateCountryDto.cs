namespace Inventory.Core.Entities.DTOs.Country
{
    public class CreateCountryDto
    {
        public string CountryName { get; set; } = null!;
        public int? Idcontinent { get; set; }
    }
}
