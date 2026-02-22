using Inventory.Core.Entities.DTOs.Country;
using System.Collections.Generic;

namespace Inventory.Core.Interfaces.Services
{
    public interface ICountryService
    {
        List<CountryDto> GetCountries();
        CountryDto GetCountryById(int id);
        CountryDto addCountry(CreateCountryDto dto);
        CountryDto updateCountry(updateCountryDto dto);
    }
}
