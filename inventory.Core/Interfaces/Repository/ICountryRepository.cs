using Inventory.Core.Entities.DTOs.Country;
using System.Collections.Generic;

namespace Inventory.Core.Interfaces.Repository
{
    public interface ICountryRepository
    {
        List<CountryDto> GetCountryList();
        CountryDto GetCountryById(int id);
        CountryDto addCountry(CreateCountryDto dto);
        CountryDto updateCountry(updateCountryDto dto);
    }
}
