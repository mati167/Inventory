namespace Inventory.Core.Entities.DTOs.Country
{
    public class updateCountryDto
    {
        public int Idcountry { get; set; }
        public string CountryName { get; set; } = null!;
        public int? Idcontinent { get; set; }
    }
}
