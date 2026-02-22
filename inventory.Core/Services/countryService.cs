using Inventory.Core.Entities.DTOs.Country;
using Inventory.Core.Interfaces.Repository;
using Inventory.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Inventory.Core.Services
{
    public class countryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ILogger<countryService> _log;

        public countryService(ICountryRepository countryRepository, ILogger<countryService> logger)
        {
            _countryRepository = countryRepository;
            _log = logger;
        }

        public List<CountryDto> GetCountries()
        {
            return _countryRepository.GetCountryList();
        }

        public CountryDto GetCountryById(int id)
        {
            return _countryRepository.GetCountryById(id);
        }

        public CountryDto addCountry(CreateCountryDto dto)
        {
            return _countryRepository.addCountry(dto);
        }

        public CountryDto updateCountry(updateCountryDto dto)
        {
            return _countryRepository.updateCountry(dto);
        }
    }
}
