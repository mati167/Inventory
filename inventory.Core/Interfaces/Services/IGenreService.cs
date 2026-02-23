using Inventory.Core.Entities.DTOs.Genre;
using System.Collections.Generic;

namespace Inventory.Core.Interfaces.Services
{
    public interface IGenreService
    {
        List<GenreDto> GetGenres();
        GenreDto GetGenreById(int id);
        GenreDto addGenre(CreateGenreDto dto);
        GenreDto updateGenre(updateGenreDto dto);
    }
}
