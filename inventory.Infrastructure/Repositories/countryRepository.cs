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
                .Include(c => c.idcontinentNavigation)
                .Include(c => c.idfilms)
                .Include(c => c.idpeople)
                .AsSplitQuery()
                .Select(c => new CountryDto
                {
                    Idcountry = c.idcountry,
                    CountryName = c.countryname,
                    Continent = c.idcontinentNavigation == null ? null : new idDescriptionDTO { Id = c.idcontinentNavigation.idcontinent, description = c.idcontinentNavigation.continentname },
                    TotalFilm = c.idfilms.Count,
                    TotalPerson = c.idpeople.Count,
                    isoCode = c.ISOCode
                })
                .OrderBy(c => c.CountryName)
                .ToList();
        }

        public CountryDto GetCountryById(int id)
        {
            _logger.LogTrace("Se obtuvo el Country");
            var country = _dbContext.Countries
                .Include(c => c.idcontinentNavigation)
                .Include(c => c.idfilms)
                .Include(c => c.idpeople)
                .AsSplitQuery()
                .Where(c => c.idcountry == id)
                .Select(c => new CountryDto
                {
                    Idcountry = c.idcountry,
                    CountryName = c.countryname,
                    Continent = c.idcontinentNavigation == null ? null : new idDescriptionDTO { Id = c.idcontinentNavigation.idcontinent, description = c.idcontinentNavigation.continentname },
                    TotalFilm = c.idfilms.Count,
                    TotalPerson = c.idpeople.Count,
                    isoCode = c.ISOCode
                }).FirstOrDefault();

            if (country == null)
                throw new Exception($"No se encontró el country con ID {id}");

            return country;
        }

        public CountryDto addCountry(CreateCountryDto dto)
        {
            var country = new Country
            {
                countryname = dto.CountryName,
                idcontinent = dto.Idcontinent
            };

            _dbContext.Countries.Add(country);
            _dbContext.SaveChanges();

            return new CountryDto
            {
                Idcountry = country.idcountry,
                CountryName = country.countryname,
                Continent = country.idcontinent == null ? null : _dbContext.Continents.Where(ct => ct.idcontinent == country.idcontinent).Select(ct => new idDescriptionDTO { Id = ct.idcontinent, description = ct.continentname }).FirstOrDefault(),
                TotalFilm = 0,
                TotalPerson = 0,
                isoCode = country.ISOCode
            };
        }

        public CountryDto updateCountry(updateCountryDto dto)
        {
            var country = _dbContext.Countries
                .Include(c => c.idfilms)
                .Include(c => c.idpeople)
                .FirstOrDefault(c => c.idcountry == dto.Idcountry);
            if (country == null)
                throw new Exception("No se encontro el country");

            country.countryname = dto.CountryName;
            country.idcontinent = dto.Idcontinent;

            _dbContext.SaveChanges();

            return new CountryDto
            {
                Idcountry = country.idcountry,
                CountryName = country.countryname,
                Continent = country.idcontinent == null ? null : _dbContext.Continents.Where(ct => ct.idcontinent == country.idcontinent).Select(ct => new idDescriptionDTO { Id = ct.idcontinent, description = ct.continentname }).FirstOrDefault(),
                TotalFilm = country.idfilms.Count,
                TotalPerson = country.idpeople.Count,
                isoCode = country.ISOCode
            };
        }
    }
}
