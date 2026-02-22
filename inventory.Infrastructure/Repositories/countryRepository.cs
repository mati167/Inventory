using Inventory.Core.Entities.DAO;
using Inventory.Core.Entities.DTOs.Country;
using Inventory.Core.Entities.DTOs.General;
using Inventory.Core.Interfaces.Repository;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.Infrastructure.Repositories
{
    public class countryRepository : ICountryRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly ILogger<countryRepository> _logger;
        public countryRepository(DatabaseContext dbContext, ILogger<countryRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public List<CountryDto> GetCountryList()
        {
            _logger.LogTrace("Se obtuvieron los Countries");
            return _dbContext.Countries
                .Include(c => c.IdcontinentNavigation)
                .Include(c => c.Idfilms)
                .Include(c => c.Idpeople)
                .Select(c => new CountryDto
                {
                    Idcountry = c.Idcountry,
                    CountryName = c.CountryName,
                    Continent = c.IdcontinentNavigation == null ? null : new idDescriptionDTO { Id = c.IdcontinentNavigation.Idcontinent, description = c.IdcontinentNavigation.ContinentName },
                    TotalFilm = c.Idfilms.Count,
                    TotalPerson = c.Idpeople.Count
                }).OrderBy(c => c.CountryName).ToList();
        }

        public CountryDto GetCountryById(int id)
        {
            _logger.LogTrace("Se obtuvo el Country");
            var country = _dbContext.Countries
                .Include(c => c.IdcontinentNavigation)
                .Include(c => c.Idfilms)
                .Include(c => c.Idpeople)
                .Where(c => c.Idcountry == id)
                .Select(c => new CountryDto
                {
                    Idcountry = c.Idcountry,
                    CountryName = c.CountryName,
                    Continent = c.IdcontinentNavigation == null ? null : new idDescriptionDTO { Id = c.IdcontinentNavigation.Idcontinent, description = c.IdcontinentNavigation.ContinentName },
                    TotalFilm = c.Idfilms.Count,
                    TotalPerson = c.Idpeople.Count
                }).FirstOrDefault();

            if (country == null)
                throw new Exception($"No se encontró el country con ID {id}");

            return country;
        }

        public CountryDto addCountry(CreateCountryDto dto)
        {
            var country = new Country
            {
                CountryName = dto.CountryName,
                Idcontinent = dto.Idcontinent
            };

            _dbContext.Countries.Add(country);
            _dbContext.SaveChanges();

            return new CountryDto
            {
                Idcountry = country.Idcountry,
                CountryName = country.CountryName,
                Continent = country.Idcontinent == null ? null : _dbContext.Continents.Where(ct => ct.Idcontinent == country.Idcontinent).Select(ct => new idDescriptionDTO { Id = ct.Idcontinent, description = ct.ContinentName }).FirstOrDefault(),
                TotalFilm = 0,
                TotalPerson = 0
            };
        }

        public CountryDto updateCountry(updateCountryDto dto)
        {
            var country = _dbContext.Countries
                .Include(c => c.Idfilms)
                .Include(c => c.Idpeople)
                .FirstOrDefault(c => c.Idcountry == dto.Idcountry);
            if (country == null)
                throw new Exception("No se encontro el country");

            country.CountryName = dto.CountryName;
            country.Idcontinent = dto.Idcontinent;

            _dbContext.SaveChanges();

            return new CountryDto
            {
                Idcountry = country.Idcountry,
                CountryName = country.CountryName,
                Continent = country.Idcontinent == null ? null : _dbContext.Continents.Where(ct => ct.Idcontinent == country.Idcontinent).Select(ct => new idDescriptionDTO { Id = ct.Idcontinent, description = ct.ContinentName }).FirstOrDefault(),
                TotalFilm = country.Idfilms.Count,
                TotalPerson = country.Idpeople.Count
            };
        }
    }
}
