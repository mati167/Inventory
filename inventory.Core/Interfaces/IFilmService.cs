using Inventory.Core.Entities.DAO;
using Inventory.Core.Entities.DTOs.Film;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.Core.Interfaces
{
    public interface IFilmService
    {
        List<FilmDto> GetFilms();
        FilmDto GetFilmById(int id);
        FilmDto addFilm(CreateFilmDto dto);
        FilmDto updateFilm(updateFilm dto);
    }
}
