using Inventory.Core.Entities.DAO;
using Inventory.Core.Entities.DTOs.Film;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Interfaces.Repository
{
    public interface IFilmRepository
    {
        List<FilmDto> GetFilmList();
        FilmDto GetFilmById(int id);
        FilmDto addFilm(CreateFilmDto dto);
        FilmDto updateFilm(updateFilm dto);
    }
}
